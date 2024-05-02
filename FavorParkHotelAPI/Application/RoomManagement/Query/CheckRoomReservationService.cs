using FPH.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;

namespace FavorParkHotelAPI.Application.RoomManagement.Services
{
    public class CheckRoomReservationService : IRequest<Response<bool>>
    {
        public CheckRoomReservationService(int roomId)
        {
            RoomId = roomId;
        }

        public int RoomId { get; }
    }

    public class CheckRoomReservationServiceHandler : BaseHandler<CheckRoomReservationService, bool>
    {
        private readonly IHotelRoomRepository _roomRepository;

        public CheckRoomReservationServiceHandler(IHotelRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public override async Task<Response<bool>> Handle(CheckRoomReservationService request, CancellationToken cancellationToken)
        {
            var roomId = request.RoomId;

            var roomEntity = await _roomRepository.GetHotelRoomByIdAsync(roomId);
            if (roomEntity == null)
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Room not found.");

            // Check if the room is reserved
            bool isReserved = roomEntity.IsReserved;

            return Success(isReserved);
        }
    }
}
