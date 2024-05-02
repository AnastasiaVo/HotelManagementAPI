using FPH.Data.Entities;
using FPH.DataBase.Abstractions;
using FPH.DataBase.Context;
using Microsoft.EntityFrameworkCore;

namespace FPH.DataBase.Repositories
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public HotelRoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HotelRoomEntity>> GetAllHotelRoomsAsync()
        {
            return await _context.HotelRooms.ToListAsync();
        }

        public async Task<int> GetRoomCapacityByIdAsync(int roomId)
        {
            var room = await _context.HotelRooms.FindAsync(roomId);
            return room?.Capacity ?? 0;
        }


        public async Task<HotelRoomEntity> GetHotelRoomByIdAsync(int id)
        {
            return await _context.HotelRooms.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddHotelRoomAsync(HotelRoomEntity hotelRoom)
        {
            _context.HotelRooms.Add(hotelRoom);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHotelRoomAsync(HotelRoomEntity hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHotelRoomAsync(int id)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(id);
            if (hotelRoom != null)
            {
                _context.HotelRooms.Remove(hotelRoom);
                await _context.SaveChangesAsync();
            }
        }
    }
}
