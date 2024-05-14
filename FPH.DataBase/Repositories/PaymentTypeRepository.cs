using FPH.Data.Entities;
using FPH.DataBase.Abstractions;
using FPH.DataBase.Context;
using Microsoft.EntityFrameworkCore;


namespace FPH.DataBase.Repositories
{
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<PaymentTypeEntity>> GetAllPaymentTypesAsync()
        {
            return await _context.PaymentTypes.ToListAsync();
        }

        public async Task<PaymentTypeEntity> GetPaymentTypeByIdAsync(int id)
        {
            return await _context.PaymentTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddPaymentTypeAsync(PaymentTypeEntity paymentType)
        {
            _context.PaymentTypes.Add(paymentType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentTypeAsync(PaymentTypeEntity paymentType)
        {
            _context.Entry(paymentType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePaymentTypeAsync(int id)
        {
            var paymentType = await _context.PaymentTypes.FindAsync(id);
            if (paymentType != null)
            {
                _context.PaymentTypes.Remove(paymentType);
                await _context.SaveChangesAsync();
            }
        }

    }
}
