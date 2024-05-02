using FPH.Data.Entities;

namespace FavorParkHotelAPI.Application.RoomManagement.Dto
{
    public class RoomDto
    {
        public int Id { get; set; }

        public int RoomNumber { get; set; }

        public int Capacity { get; set; }

        public bool IsReserved { get; set; } = false;

        public int AccomodationTypeEntityId { get; set; }

        public int BookingId { get; set; }

    }
}
