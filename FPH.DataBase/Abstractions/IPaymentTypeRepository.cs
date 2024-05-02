using FPH.Data.Entities;

namespace FPH.DataBase.Abstractions
{
    public interface IPaymentTypeRepository
    {
        Task<PaymentTypeEntity> GetByIdAsync(int id);
        Task<IEnumerable<PaymentTypeEntity>> GetAllAsync();
        Task AddAsync(PaymentTypeEntity paymentType);
        Task UpdateAsync(PaymentTypeEntity paymentType);
        Task DeleteAsync(PaymentTypeEntity paymentType);
    }
}
