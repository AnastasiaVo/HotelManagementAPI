using FPH.Data.Entities;

namespace FavorParkHotelAPI.Application.BookingManagement
{
    public class RoomFeeCalculationService
    {
        private const decimal StandardRoomRate = 1800m;
        private const decimal BusinessRoomRate = 2550m;
        private const decimal SuiteRoomRate = 3500m;

        public decimal CalculateRoomFeePerNight(int numberOfGuests, int numberOfChildren, AccomodationTypeEntity roomType)
        {
            decimal baseRate = roomType.Name switch
            {
                "Standard" => StandardRoomRate,
                "Business" => BusinessRoomRate,
                "Suite" => SuiteRoomRate,
                _ => throw new ArgumentException("Invalid room type"),
            };

            //Additional charges for extra guests or children
            if (numberOfGuests > 2 || numberOfChildren > 0)
            {
                baseRate += (numberOfGuests - 2) * 500m; // Additional charge per extra adult
                baseRate += numberOfChildren * 300m; // Additional charge per child
            }

            return baseRate;
        }
    }
}
