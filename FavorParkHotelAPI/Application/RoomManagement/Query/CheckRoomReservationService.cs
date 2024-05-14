using FPH.DataBase.Abstractions;
using FPH.Common;
using MediatR;

namespace FavorParkHotelAPI.Application.RoomManagement.Services
{
    public class CheckRoomReservationService : IRequest<Response<string>>
    {
        public CheckRoomReservationService(int roomId, DateTime startDate, DateTime endDate)
        {
            RoomId = roomId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int RoomId { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
    }

    public class CheckRoomReservationServiceHandler : BaseHandler<CheckRoomReservationService, string>
    {
        private readonly IBookingRepository _bookingRepository;

        public CheckRoomReservationServiceHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public override async Task<Response<string>> Handle(CheckRoomReservationService request, CancellationToken cancellationToken)
        {
            var roomId = request.RoomId;
            var startDate = request.StartDate;
            var endDate = request.EndDate;

            // Retrieve bookings for the specified date range
            var bookedRooms = await _bookingRepository.SearchBookingsByDatesAsync(startDate, endDate);

            // Check if the room with the specified ID is booked during the specified date range
            var isRoomBooked = bookedRooms.Any(b => b.RoomId == roomId);

            if (!isRoomBooked)
            {
                return Success("The room is free");
            }
            return Success("The room is reserved");
        }
    }
}

