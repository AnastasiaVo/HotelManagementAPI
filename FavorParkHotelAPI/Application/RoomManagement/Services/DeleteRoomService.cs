using FPH.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;

namespace FavorParkHotelAPI.Application.RoomManagement.Services
{
    public class DeleteRoomService : IRequest<Response<bool>>
    {
        public DeleteRoomService(int roomId)
        {
            RoomId = roomId;
        }

        public int RoomId { get; }
    }

    public class DeleteRoomServiceHandler : BaseHandler<DeleteRoomService, bool>
    {
        private readonly IHotelRoomRepository _roomRepository;

        public DeleteRoomServiceHandler(IHotelRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public override async Task<Response<bool>> Handle(DeleteRoomService request, CancellationToken cancellationToken)
        {
            var roomId = request.RoomId;

            var roomEntity = await _roomRepository.GetHotelRoomByIdAsync(roomId);
            if (roomEntity == null)
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Room not found.");

            // Check if the room is associated with any booking
            if (roomEntity.BookingId != 0)
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Cannot delete. Room is associated with a booking.");

            await _roomRepository.DeleteHotelRoomAsync(roomId);

            return Success(true);
        }
    }
}

