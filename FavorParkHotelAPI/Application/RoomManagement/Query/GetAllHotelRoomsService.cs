using FPH.Common;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FPH.Data.Entities;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;
using FavorParkHotelAPI.Application.RoomManagement.Dto;

namespace FavorParkHotelAPI.Application.RoomManagement.Query
{
    public class GetAllHotelRoomsService : IRequest<Response<IEnumerable<RoomDto>>>
    {
    }

    public class GetAllHotelRoomsServiceHandler : BaseHandler<GetAllHotelRoomsService, IEnumerable<RoomDto>>
    {
        private readonly IHotelRoomRepository _roomRepository;

        public GetAllHotelRoomsServiceHandler(IHotelRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public override async Task<Response<IEnumerable<RoomDto>>> Handle(GetAllHotelRoomsService request, CancellationToken cancellationToken)
        {
            var hotelRooms = await _roomRepository.GetAllHotelRoomsAsync();
            var dtoList = hotelRooms.Select(room => new RoomDto
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                Capacity = room.Capacity,
                IsReserved = room.IsReserved,
                AccomodationTypeEntityId = room.AccomodationTypeEntityId,
                BookingId = room.BookingId
            }).ToList();

            return Success(dtoList);
        }
    }
}


