using FavorParkHotelAPI.Application.Authentification.Dto;
using FavorParkHotelAPI.Application.RoomManagement.Dto;
using FPH.Common;
using FPH.Data.Entities;
using FPH.DataBase.Context;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FavorParkHotelAPI.Application.RoomManagement.Services
{
    public class CreateRoomService : IRequest<Response<RoomDto>>
    {
        public CreateRoomService(ApplyRoomDto roomDto)
        {
            RoomDto = roomDto;
        }

        public ApplyRoomDto RoomDto { get; }
    }

    public class Handler : BaseHandler<CreateRoomService, RoomDto>
    {
        private readonly ApplicationDbContext _dbContext;

        public Handler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<Response<RoomDto>> Handle(CreateRoomService request, CancellationToken cancellationToken)
        {
            var roomDto = request.RoomDto;
            var roomEntity = new HotelRoomEntity
            {
                RoomNumber = roomDto.RoomNumber,
                Capacity = roomDto.Capacity,
                IsReserved = roomDto.IsReserved,
                AccomodationTypeEntityId = roomDto.AccomodationTypeEntityId,
                BookingId = roomDto.BookingId
            };

            _dbContext.HotelRooms.Add(roomEntity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var createdRoomDto = new RoomDto
            {
                Id = roomEntity.Id,
                RoomNumber = roomEntity.RoomNumber,
                Capacity = roomEntity.Capacity,
                IsReserved = roomEntity.IsReserved,
                AccomodationTypeEntityId = roomEntity.AccomodationTypeEntityId,
                BookingId = roomEntity.BookingId
            };

            //var result = new RoomDto(roomDto);

            return Success(createdRoomDto);
        }
    }
}

