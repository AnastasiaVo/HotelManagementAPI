using FPH.Common;
using FPH.Data.Entities;
using FavorParkHotelAPI.Application.BookingManagement.Dto;
using FavorParkHotelAPI.Application.PaymentManagement.Dto;
using FavorParkHotelAPI.Application.PaymentManagement.Service;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using FPH.DataBase.Context;
using FavorParkHotelAPI.Application.BookingManagement.Mapper;
using FavorParkHotelAPI.Application.RoomManagement.Services;

namespace FavorParkHotelAPI.Application.BookingManagement.Services
{
    public class CreateBookingService : IRequest<Response<BookingDto>>
    {
        public CreateBookingService(CreateBookingDto bookingDto)
        {
            BookingDto = bookingDto;
        }

        public CreateBookingDto BookingDto { get; }
    }

    public class Handler : BaseHandler<CreateBookingService, BookingDto>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMediator _mediator;
        private readonly RoomFeeCalculationService _roomFeeCalculationService;
        private readonly ApplicationDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IHotelRoomRepository _hotelRoomRepository;

        public Handler(IBookingRepository bookingRepository, IMediator mediator,
            RoomFeeCalculationService roomFeeCalculationService, ApplicationDbContext applicationDbContext,
            IUserRepository userRepository, IHotelRoomRepository hotelRoomRepository)
        {
            _bookingRepository = bookingRepository;
            _mediator = mediator;
            _roomFeeCalculationService = roomFeeCalculationService;
            _context = applicationDbContext;
            _userRepository = userRepository;
            _hotelRoomRepository = hotelRoomRepository;
        }

        public override async Task<Response<BookingDto>> Handle(CreateBookingService request, CancellationToken cancellationToken)
        {
            try
            {
                var bookingDto = request.BookingDto;

                // Parse date strings into DateTime objects
                var checkInDate = DateTime.Parse(bookingDto.CheckInDate);
                var checkOutDate = DateTime.Parse(bookingDto.CheckOutDate);

                var user = await _userRepository.GetUserByIdAsync(bookingDto.UserId);
                if (user == null)
                {
                    throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Logged in user not found");
                }

                var room = await _hotelRoomRepository.GetHotelRoomByIdAsync(bookingDto.RoomId);
                if (room == null)
                {
                    throw new ProblemDetailsException(StatusCodes.Status404NotFound, $"Room with ID {bookingDto.RoomId} not found");
                }

                // Check room availability for the specified dates
                var roomReservationCheckService = new CheckRoomReservationService(room.Id, checkInDate, checkOutDate);
                var roomReservationResponse = await _mediator.Send(roomReservationCheckService);
                if (roomReservationResponse.Result != "The room is free")
                {
                    throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Room is not available for the specified dates.");
                }

                var roomFeePerNight = _roomFeeCalculationService.CalculateRoomFeePerNight(bookingDto.NumberOfGuests,
                    bookingDto.NumberOfChildren, room.AccomodationTypeEntity);

                var bookingEntity = new BookingEntity
                {
                    User = user,
                    HotelRooms = room,
                    NumberOfGuests = bookingDto.NumberOfGuests,
                    NumberOfChildren = bookingDto.NumberOfChildren,
                    CheckInDate = checkInDate,
                    CheckOutDate = checkOutDate,
                    RoomFeePerNight = roomFeePerNight,
                };

                await _bookingRepository.AddBookingAsync(bookingEntity);
                await _context.SaveChangesAsync();

                // Update Room IsReserved status to true
                room.IsReserved = true;
                await _context.SaveChangesAsync();

                // Calculate payment amount based on room fee per night and duration of stay
                var durationOfStay = (checkOutDate - checkInDate).Days;
                var paymentAmount = roomFeePerNight * durationOfStay;

                // Create Payment for the Booking
                var paymentDto = new CreatePaymentDto
                {
                    BookingId = bookingEntity.Id,
                    PaymentTypeId = 1, // By card
                    PaymentAmount = paymentAmount,
                    IsActive = true,
                    IsPaid = true // Payment is paid initially
                };

                var paymentResponse = await _mediator.Send(new CreatePaymentService(paymentDto));

                if (paymentResponse.IsSuccess)
                {
                    // Update Booking with PaymentId
                    bookingEntity.PaymentEntityId = paymentResponse.Result.Id;
                    await _bookingRepository.UpdateBookingAsync(bookingEntity);

                    await _context.SaveChangesAsync();
                    return Success(bookingEntity.MapToDto());
                }
                else
                {
                    // If payment creation fails, handle the error accordingly
                    throw new ProblemDetailsException(StatusCodes.Status500InternalServerError, "An error occurred while creating the payment.");
                }
            }
            catch (Exception ex)
            {
                throw new ProblemDetailsException(StatusCodes.Status500InternalServerError, "An error occurred while creating the booking.", ex);
            }
        }
    }
}



