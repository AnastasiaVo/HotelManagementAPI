using FPH.DataBase.Abstractions;
using FPH.DataBase.Context;
using FPH.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FPH.DataBase.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentEntity> GetByIdAsync(int id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public async Task<PaymentEntity> GetPaymentByBookingIdAsync(int bookingId)
        {
            return await _context.Payments.FirstOrDefaultAsync(p => p.BookingId == bookingId);
        }

        public async Task<IEnumerable<PaymentEntity>> GetAllAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task AddAsync(PaymentEntity payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PaymentEntity payment)
        {
            _context.Entry(payment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PaymentEntity payment)
        {
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
        }
    }
}
