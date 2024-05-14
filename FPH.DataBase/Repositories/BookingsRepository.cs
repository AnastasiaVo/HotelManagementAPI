using FPH.Data.Entities;
using FPH.DataBase.Abstractions;
using FPH.DataBase.Context;
using Microsoft.EntityFrameworkCore;

namespace FPH.DataBase.Repositories
{
    public class BookingsRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookingEntity>> GetAllBookingsAsync()
        {
            return await _context.HotelBookings.ToListAsync();
        }

        public async Task<BookingEntity> GetBookingByIdAsync(int id)
        {
            return await _context.HotelBookings.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddBookingAsync(BookingEntity booking)
        {
            _context.HotelBookings.Add(booking);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateBookingAsync(BookingEntity booking)
        {
            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _context.HotelBookings.FindAsync(id);
            if (booking != null)
            {
                _context.HotelBookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<BookingEntity>> SearchBookingsByGuestNameAsync(string guestName)
        {
            return await _context.HotelBookings.Where(b => b.User.LastName == guestName).ToListAsync();
        }

        public async Task<IEnumerable<BookingEntity>> SearchBookingsByDatesAsync(DateTime startDate, DateTime endDate)
        {
            // Ensure that the comparison includes bookings that overlap with the specified date range
            return await _context.HotelBookings
                .Where(b => b.CheckInDate <= endDate && b.CheckOutDate >= startDate)
                .ToListAsync();
        }
    }
}
