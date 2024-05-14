using FPH.Common;
using FPH.DataBase.Abstractions;
using FavorParkHotelAPI.Application.RoomManagement.Dto;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FavorParkHotelAPI.Application.RoomManagement.Query
{
    public class GetFreeHotelRoomsByCapacityService : IRequest<Response<IEnumerable<RoomDto>>>
    {
        public GetFreeHotelRoomsByCapacityService(int capacity)
        {
            Capacity = capacity;
        }

        public int Capacity { get; }
    }

    public class GetFreeHotelRoomsByCapacityServiceHandler : BaseHandler<GetFreeHotelRoomsByCapacityService, IEnumerable<RoomDto>>
    {
        private readonly IHotelRoomRepository _roomRepository;

        public GetFreeHotelRoomsByCapacityServiceHandler(IHotelRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public override async Task<Response<IEnumerable<RoomDto>>> Handle(GetFreeHotelRoomsByCapacityService request, CancellationToken cancellationToken)
        {
            var rooms = await _roomRepository.GetFreeHotelRoomsByCapacityAsync(request.Capacity);

            if (rooms == null || !rooms.Any())
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "No free rooms found with the specified capacity.");
            }

            var roomDtos = rooms.Select(room => new RoomDto
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                Capacity = room.Capacity,
                IsReserved = room.IsReserved,
                AccomodationTypeEntityId = room.AccomodationTypeEntityId
            });

            return Success(roomDtos);
        }
    }
}

