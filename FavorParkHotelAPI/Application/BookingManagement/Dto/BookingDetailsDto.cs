using FavorParkHotelAPI.Application.RoomManagement.Dto;

namespace FavorParkHotelAPI.Application.BookingManagement.Dto
{
    public class BookingDetailsDto
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        public string UserId { get; set; }

        public int NumberOfGuests { get; set; }

        public int NumberOfChildren { get; set; }

        public string CheckInDate { get; set; }

        public string CheckOutDate { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
