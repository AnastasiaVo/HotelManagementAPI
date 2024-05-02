namespace FPH.Data.Entities
{
    public class PaymentEntity
    {
        public int Id { get; set; }

        public int BookingId { get; set; }

        public virtual BookingEntity? Booking { get; set; }

        public int PaymentTypeId { get; set; }
        public virtual PaymentTypeEntity PaymentType { get; set; }

        public decimal PaymentAmount { get; set; } = decimal.Zero;

        public bool IsActive { get; set; }
    }
}
