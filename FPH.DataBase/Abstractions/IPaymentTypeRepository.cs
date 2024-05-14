using FPH.Data.Entities;

namespace FPH.DataBase.Abstractions
{
    public interface IPaymentTypeRepository
    {
        Task<IEnumerable<PaymentTypeEntity>> GetAllPaymentTypesAsync();
        Task<PaymentTypeEntity> GetPaymentTypeByIdAsync(int id);
        Task AddPaymentTypeAsync(PaymentTypeEntity paymentType);
        Task UpdatePaymentTypeAsync(PaymentTypeEntity paymentType);
        Task DeletePaymentTypeAsync(int id);
    }
}
