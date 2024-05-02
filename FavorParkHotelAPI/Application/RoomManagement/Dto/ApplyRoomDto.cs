using System.ComponentModel.DataAnnotations;

namespace FavorParkHotelAPI.Application.RoomManagement.Dto
{
    public class ApplyRoomDto
    {
        public int Id { get; set; } // Add Id property for updates

        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Required]
        public bool IsReserved { get; set; } = false;

        [Required]
        public int AccomodationTypeEntityId { get; set; }

        public int BookingId { get; set; }
    }
}

