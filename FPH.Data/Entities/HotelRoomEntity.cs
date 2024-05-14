
namespace FPH.Data.Entities
{
    public class HotelRoomEntity
    {
        public int Id { get; set; }

        public int RoomNumber { get; set; }

        public int Capacity { get; set; }

        public bool IsReserved { get; set; } = false;

        public int AccomodationTypeEntityId { get; set; }

        public virtual AccomodationTypeEntity? AccomodationTypeEntity { get; set; }

        public virtual UserEntity? Users { get; set; } 

        public int? BookingId { get; set; }

        public virtual BookingEntity? Booking { get; set; }
    }
}