namespace FPH.Data.Entities
{
    public class BookingEntity
    {
        public int Id { get; set; }

        public virtual List<HotelRoomEntity> HotelRoomEntities { get; set; } = new();

        public string UserId { get; set; } = string.Empty;

        public virtual UserEntity? User { get; set; }

        public int NumberOfGuests { get; set; }

        public int NumberOfChildren { get; set; } = 0;

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public decimal RoomFeePerNight { get; set; } = decimal.Zero;

        public int PaymentEntityId { get; set; }

        public virtual PaymentEntity? PaymentEntity { get; set; }
    }
}
