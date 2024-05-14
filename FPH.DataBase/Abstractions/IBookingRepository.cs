using FPH.Data.Entities;

namespace FPH.DataBase.Abstractions
{
    public interface IBookingRepository
    {
        Task<IEnumerable<BookingEntity>> GetAllBookingsAsync();
        Task<BookingEntity> GetBookingByIdAsync(int id);
        Task AddBookingAsync(BookingEntity booking);
        Task UpdateBookingAsync(BookingEntity booking);
        Task DeleteBookingAsync(int id);
        Task<IEnumerable<BookingEntity>> SearchBookingsByGuestNameAsync(string guestName);
        Task<IEnumerable<BookingEntity>> SearchBookingsByDatesAsync(DateTime startDate, DateTime endDate);
    }
}
