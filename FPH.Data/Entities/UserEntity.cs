using Microsoft.AspNetCore.Identity;

namespace FPH.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        //public int? HotelRoomId { get; set; }
        public int? TempPassword { get; set; }

        public virtual List<HotelRoomEntity> HotelRoom { get; set; }

        public virtual List<BookingEntity> Bookings { get; set; } = new();

    }
}
