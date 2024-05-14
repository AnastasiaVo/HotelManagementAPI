using Hellang.Middleware.ProblemDetails;
using FavorParkHotelAPI.Application.UserManagement.Dto;
using FPH.DataBase.Abstractions;
using Microsoft.AspNetCore.Identity;
using FPH.Data.Entities;

namespace FavorParkHotelAPI.Application.UserManagement;

public static class UserHelper
{
    public static async Task<UserDto> GetUserByIdAsync(string userId, string userThatGets, IUserRepository userRepository, UserManager<UserEntity> _userManager)
    {
        var user = await userRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Requested user not found");
        }

        var loggedInUser = await userRepository.GetUserByIdAsync(userThatGets);
        if (loggedInUser == null)
        {
            throw new ProblemDetailsException(StatusCodes.Status404NotFound, "Logged in user not found");
        }

        var userDto = user.MapToUserModel();
        userDto.Admin = await _userManager.IsInRoleAsync(loggedInUser, "Admin");

        return userDto;
    }
}
