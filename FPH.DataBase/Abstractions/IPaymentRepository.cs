using FPH.Data.Entities;

namespace FPH.DataBase.Abstractions
{
    public interface IPaymentRepository
    {
        Task<PaymentEntity> GetByIdAsync(int id);
        Task<IEnumerable<PaymentEntity>> GetAllAsync();
        Task AddAsync(PaymentEntity payment);
        Task UpdateAsync(PaymentEntity payment);
        Task DeleteAsync(PaymentEntity payment);
    }
}
