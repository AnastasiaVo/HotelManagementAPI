using FavorParkHotelAPI.Application.UserManagement.Credentials.Dto;
using FavorParkHotelAPI.Application.UserManagement.Dto;
using FPH.Data.Entities;

namespace FavorParkHotelAPI.Application.UserManagement;
public static class UserMapper
{
    public static UserLoginGetModel MapToUserLoginGetModel(this UserEntity user)
    {
        return new UserLoginGetModel
        {
            Id = user.Id,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };
    }

    public static UserDto MapToUserModel(this UserEntity user)
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            Email = user.Email ?? string.Empty,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
        };
    }

    public static UserEntity MapToUser(this UserDto user)
    {
        return new UserEntity
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber= user.PhoneNumber,
            HotelRoom = new List<HotelRoomEntity>(),
        };
    }
}
