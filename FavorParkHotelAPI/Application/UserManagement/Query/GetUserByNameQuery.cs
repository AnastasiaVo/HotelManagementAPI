using MediatR;
using FavorParkHotelAPI.Application.UserManagement.Dto;
using FPH.Common;
using FavorParkHotelAPI;
using FPH.DataBase.Abstractions;
using FavorParkHotelAPI.Application.UserManagement;
using FPH.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FavorParkHotelAPI.Application.UserManagement.Query;

public class GetUserByNameQuery : IRequest<Response<IEnumerable<UserDto>>>
{
    public string UserName { get; }
    public string LoggedInUser { get; }

    public GetUserByNameQuery(string userName, string loggedInUser)
    {
        UserName = userName;
        LoggedInUser = loggedInUser;
    }

    public class Handler : BaseHandler<GetUserByNameQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        public UserManager<UserEntity> _userManager { get; }
        public Handler(IUserRepository userRepository, UserManager<UserEntity> entity)
        {
            _userRepository = userRepository;
            _userManager = entity;
        }


        public override async Task<Response<IEnumerable<UserDto>>> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUserBySurnameAsync(request.UserName);
            var userModels = new List<UserDto>();

            foreach (var u in users)
            {
                userModels.Add(await UserHelper.GetUserByIdAsync(u.Id, request.LoggedInUser, _userRepository, _userManager));
            }

            return Success(userModels);
        }
    }
}