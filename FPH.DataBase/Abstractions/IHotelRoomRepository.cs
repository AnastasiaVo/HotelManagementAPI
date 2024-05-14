using FPH.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPH.DataBase.Abstractions
{
    public interface IHotelRoomRepository
    {
        Task<int> GetRoomCapacityByIdAsync(int roomId); //get the room capacity by room ID
        Task<IEnumerable<HotelRoomEntity>> GetAllHotelRoomsAsync();
        Task<HotelRoomEntity> GetHotelRoomByIdAsync(int roomId);
        Task AddHotelRoomAsync(HotelRoomEntity hotelRoom);
        Task UpdateHotelRoomAsync(HotelRoomEntity hotelRoom);
        Task DeleteHotelRoomAsync(int id);
        Task<IEnumerable<HotelRoomEntity>> GetFreeHotelRoomsByCapacityAsync(int capacity);
    }
}
