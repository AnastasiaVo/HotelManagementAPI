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

        public async Task<PaymentTypeEntity> GetByIdAsync(int id)
        {
            return await _context.PaymentTypes.FindAsync(id);
        }

        public async Task<IEnumerable<PaymentTypeEntity>> GetAllAsync()
        {
            return await _context.PaymentTypes.ToListAsync();
        }

        public async Task AddAsync(PaymentTypeEntity paymentType)
        {
            _context.PaymentTypes.Add(paymentType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PaymentTypeEntity paymentType)
        {
            _context.Entry(paymentType).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PaymentTypeEntity paymentType)
        {
            _context.PaymentTypes.Remove(paymentType);
            await _context.SaveChangesAsync();
        }
    }
}
