namespace FPH.Data.Entities
{
    public class PaymentTypeEntity
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual PaymentEntity? Payment { get; set; }
    }
}
