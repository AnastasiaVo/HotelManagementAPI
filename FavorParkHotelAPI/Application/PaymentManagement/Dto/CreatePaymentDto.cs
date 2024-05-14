namespace FavorParkHotelAPI.Application.PaymentManagement.Dto
{
    public class CreatePaymentDto
    {
        public int BookingId { get; set; }

        public int PaymentTypeId { get; set; }

        public decimal PaymentAmount { get; set; }

        public bool IsActive { get; set; }

        public bool IsPaid { get; set; }
    }
}
