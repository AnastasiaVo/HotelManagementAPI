using FavorParkHotelAPI.Application.UserManagement;
using MediatR;
using FavorParkHotelAPI.Application.UserManagement.Dto;
using FPH.Common;
using FavorParkHotelAPI;
using FPH.DataBase.Abstractions;
using FPH.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FavorParkHotelAPI.Application.UserManagement.Query;

public class GetUserQuery : IRequest<Response<UserDto>>
{
    public string UserId { get; }
    public string LoggedInUser { get; }

    public GetUserQuery(string userId, string loggedInUser)
    {
        UserId = userId;
        LoggedInUser = loggedInUser;
    }

    public class Handler : BaseHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        public UserManager<UserEntity> _userManager { get; }
        public Handler(IUserRepository userRepository, UserManager<UserEntity> entity)
        {
            _userRepository = userRepository;
            _userManager = entity;
        }

        public override async Task<Response<UserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var userModel = await UserHelper.GetUserByIdAsync(request.UserId, request.LoggedInUser, _userRepository, _userManager);

            return Success(userModel);
        }
    }
}