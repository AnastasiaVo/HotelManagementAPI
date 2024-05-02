using FPH.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;

namespace FavorParkHotelAPI.Application.RoomManagement.Query
{
    public class GetRoomCapacityByIdService : IRequest<Response<int>>
    {
        public GetRoomCapacityByIdService(int roomId)
        {
            RoomId = roomId;
        }

        public int RoomId { get; }
    }

    public class GetRoomCapacityByIdServiceHandler : BaseHandler<GetRoomCapacityByIdService, int>
    {
        private readonly IHotelRoomRepository _roomRepository;

        public GetRoomCapacityByIdServiceHandler(IHotelRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public override async Task<Response<int>> Handle(GetRoomCapacityByIdService request, CancellationToken cancellationToken)
        {
            var capacity = await _roomRepository.GetRoomCapacityByIdAsync(request.RoomId);
            return Success(capacity);
        }
    }
}

