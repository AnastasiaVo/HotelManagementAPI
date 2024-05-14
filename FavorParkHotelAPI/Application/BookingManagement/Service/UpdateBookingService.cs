using FPH.Common;
using FavorParkHotelAPI.Application.BookingManagement.Dto;
using FPH.Data.Entities;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using FPH.DataBase.Context;
using FavorParkHotelAPI.Application.RoomManagement.Services;
using Microsoft.AspNetCore.Components.Forms;

namespace FavorParkHotelAPI.Application.BookingManagement.Services
{
    public class UpdateBookingService : IRequest<Response<BookingDto>>
    {
        public UpdateBookingService(int bookingId, CreateBookingDto bookingDto)
        {
            BookingId = bookingId;
            BookingDto = bookingDto;
        }

        public int BookingId { get; }
        public CreateBookingDto BookingDto { get; }
    }

    public class UpdateBookingServiceHandler : BaseHandler<UpdateBookingService, BookingDto>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMediator _mediator;
        private readonly RoomFeeCalculationService _roomFeeCalculationService;
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IHotelRoomRepository _hotelRoomRepository;

        public UpdateBookingServiceHandler(IBookingRepository bookingRepository, RoomFeeCalculationService roomFeeCalculationService,
            ApplicationDbContext applicationDbContext, IUserRepository userRepository, IHotelRoomRepository hotelRoomRepository,
            IMediator mediator)
        {
            _bookingRepository = bookingRepository;
            _roomFeeCalculationService = roomFeeCalculationService;
            _context = applicationDbContext;
            _userRepository = userRepository;
            _hotelRoomRepository = hotelRoomRepository;
            _mediator = mediator;
        }

        public override async Task<Response<BookingDto>> Handle(UpdateBookingService request, CancellationToken cancellationToken)
        {
            var bookingId = request.BookingId;
            var bookingDto = request.BookingDto;

            var bookingEntity = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (bookingEntity == null)
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Booking not found.");

            var room = await _hotelRoomRepository.GetHotelRoomByIdAsync(bookingDto.RoomId);
            if (room == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, $"Room with ID {bookingDto.RoomId} not found");
            }


            // Update the entity with the new data
            bookingEntity.UserId = bookingDto.UserId;
            bookingEntity.RoomId = bookingDto.RoomId;
            bookingEntity.NumberOfGuests = bookingDto.NumberOfGuests;
            bookingEntity.NumberOfChildren = bookingDto.NumberOfChildren;
            bookingEntity.CheckInDate = DateTime.Parse(bookingDto.CheckInDate);
            bookingEntity.CheckOutDate = DateTime.Parse(bookingDto.CheckOutDate);

            var roomReservationCheckService = new CheckRoomReservationService(room.Id, bookingEntity.CheckInDate, bookingEntity.CheckOutDate);
            var roomReservationResponse = await _mediator.Send(roomReservationCheckService);
            if (roomReservationResponse.Result != "The room is free")
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Room is not available for the specified dates.");
            }

            var roomFeePerNight = _roomFeeCalculationService.CalculateRoomFeePerNight(bookingDto.NumberOfGuests,
                bookingDto.NumberOfChildren, room.AccomodationTypeEntity);


            await _bookingRepository.UpdateBookingAsync(bookingEntity);

            return Success(new BookingDto
            {
                Id = bookingEntity.Id,
                RoomId = bookingEntity.RoomId,
                UserId = bookingEntity.UserId,
                NumberOfGuests = bookingEntity.NumberOfGuests,
                NumberOfChildren = bookingEntity.NumberOfChildren,
                CheckInDate = bookingEntity.CheckInDate.ToString("yyyy-MM-dd"),
                CheckOutDate = bookingEntity.CheckOutDate.ToString("yyyy-MM-dd"),
                RoomFeePerNight = roomFeePerNight,
                PaymentId = bookingEntity.PaymentEntityId
            });
        }
    }
}

