using FPH.Common;
using MediatR;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;
using FavorParkHotelAPI.Application.RoomManagement.Dto;


namespace FavorParkHotelAPI.Application.RoomManagement.Query
{
    public class GetHotelRoomByIdService : IRequest<Response<RoomDto>>
    {
        public GetHotelRoomByIdService(int roomId)
        {
            RoomId = roomId;
        }

        public int RoomId { get; }
    }

    public class GetHotelRoomByIdServiceHandler : BaseHandler<GetHotelRoomByIdService, RoomDto>
    {
        private readonly IHotelRoomRepository _roomRepository;

        public GetHotelRoomByIdServiceHandler(IHotelRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public override async Task<Response<RoomDto>> Handle(GetHotelRoomByIdService request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.GetHotelRoomByIdAsync(request.RoomId);
            if (room == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Room not found.");
            }

            var dto = new RoomDto
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                Capacity = room.Capacity,
                IsReserved = room.IsReserved,
                AccomodationTypeEntityId = room.AccomodationTypeEntityId,
                //BookingId = room.BookingId
            };

            return Success(dto);
        }
    }
}

