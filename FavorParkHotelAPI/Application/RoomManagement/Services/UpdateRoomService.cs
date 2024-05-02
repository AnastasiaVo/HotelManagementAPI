using FPH.Common;
using MediatR;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;
using FavorParkHotelAPI.Application.RoomManagement.Dto;

namespace FavorParkHotelAPI.Application.RoomManagement.Services
{
    public class UpdateRoomService : IRequest<Response<RoomDto>>
    {
        public UpdateRoomService(int roomId, ApplyRoomDto roomDto)
        {
            RoomId = roomId;
            RoomDto = roomDto;
        }

        public int RoomId { get; }
        public ApplyRoomDto RoomDto { get; }
    }

    public class UpdateRoomServiceHandler : BaseHandler<UpdateRoomService, RoomDto>
    {
        private readonly IHotelRoomRepository _roomRepository;

        public UpdateRoomServiceHandler(IHotelRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public override async Task<Response<RoomDto>> Handle(UpdateRoomService request, CancellationToken cancellationToken)
        {
            var roomId = request.RoomId;
            var roomDto = request.RoomDto;

            var roomEntity = await _roomRepository.GetHotelRoomByIdAsync(roomId);
            if (roomEntity == null)
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Room not found.");

            // Update the entity with the new data
            roomEntity.RoomNumber = roomDto.RoomNumber;
            roomEntity.Capacity = roomDto.Capacity;
            roomEntity.IsReserved = roomDto.IsReserved;
            roomEntity.AccomodationTypeEntityId = roomDto.AccomodationTypeEntityId;
            roomEntity.BookingId = roomDto.BookingId;

            await _roomRepository.UpdateHotelRoomAsync(roomEntity);

            return Success(new RoomDto
            {
                Id = roomEntity.Id,
                RoomNumber = roomEntity.RoomNumber,
                Capacity = roomEntity.Capacity,
                IsReserved = roomEntity.IsReserved,
                AccomodationTypeEntityId = roomEntity.AccomodationTypeEntityId,
                BookingId = roomEntity.BookingId
            });
        }
    }
}
