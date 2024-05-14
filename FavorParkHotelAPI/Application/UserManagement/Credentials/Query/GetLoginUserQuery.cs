using MediatR;
using FPH.Common;
using Microsoft.AspNetCore.Identity;
using FPH.Data.Entities;
using FavorParkHotelAPI.Application.UserManagement.Credentials.Dto;
using FavorParkHotelAPI;
using FavorParkHotelAPI.Application.UserManagement;

namespace FavorParkHotelAPI.Application.UserManagement.Credentials.Query;

public class GetLoginUserQuery : IRequest<Response<UserLoginGetModel>>
{
    public string UserId { get; }

    public GetLoginUserQuery(string userId)
    {
        UserId = userId;
    }
    public class Handler : BaseHandler<GetLoginUserQuery, UserLoginGetModel>
    {
        public UserManager<UserEntity> _userManager { get; }


        public Handler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<Response<UserLoginGetModel>> Handle(GetLoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            return Success(user.MapToUserLoginGetModel());
        }
    }
}
