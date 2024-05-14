using FavorParkHotelAPI.Application.BookingManagement.Dto;
using FPH.Data.Entities;
using FPH.DataBase.Abstractions;

namespace FavorParkHotelAPI.Application.BookingManagement.Mapper
{
    public static class BookingMapper
    {
        public static BookingDto MapToDto(this BookingEntity bookingEntity)
        {
            return new BookingDto
            {
                Id = bookingEntity.Id,
                NumberOfGuests = bookingEntity.NumberOfGuests,
                NumberOfChildren = bookingEntity.NumberOfChildren,
                CheckInDate = bookingEntity.CheckInDate.ToString("yyyy-MM-dd"), 
                CheckOutDate = bookingEntity.CheckOutDate.ToString("yyyy-MM-dd"),
            };
        }
    }
}
