using MediatR;
using Microsoft.EntityFrameworkCore;
using FPH.Common;
using FavorParkHotelAPI.Application.UserManagement.Dto;
using FPH.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FavorParkHotelAPI.Application.UserManagement.Query;

public class IsExistQuery : IRequest<Response<bool>>
{
    public UserLoginDto Model { get; }

    public IsExistQuery(UserLoginDto model)
    {
        Model = model;
    }

    public class Handler : BaseHandler<IsExistQuery, bool>
    {
        public UserManager<UserEntity> _userManager { get; }

        public Handler(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<Response<bool>> Handle(IsExistQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Model.Email, cancellationToken);

            return Success(user != null);
        }
    }
}
