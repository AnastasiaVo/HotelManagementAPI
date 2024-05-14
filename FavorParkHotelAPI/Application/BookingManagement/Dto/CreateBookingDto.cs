namespace FavorParkHotelAPI.Application.BookingManagement.Dto
{
    public class CreateBookingDto
    {
        public string UserId { get; set; }

        public int RoomId { get; set; }

        public int NumberOfGuests { get; set; }

        public int NumberOfChildren { get; set; }

        public string CheckInDate { get; set; }

        public string CheckOutDate { get; set; }
    }
}
