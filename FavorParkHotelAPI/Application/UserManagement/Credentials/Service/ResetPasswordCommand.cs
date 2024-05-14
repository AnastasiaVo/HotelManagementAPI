using MediatR;
using FPH.Common;
using FavorParkHotelAPI.Application.UserManagement.Credentials.Dto;
using FPH.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Hellang.Middleware.ProblemDetails;
using FPH.DataBase.Context;

namespace FavorParkHotelAPI.Application.UserManagement.Credentials.Service
{
    public class ResetPasswordCommand : IRequest<Response<Unit>>
    {
        public ResetPasswordModel Model { get; }

        public ResetPasswordCommand(ResetPasswordModel model)
        {
            Model = model;
        }

        public class Handler : BaseHandler<ResetPasswordCommand, Unit>
        {
            public UserManager<UserEntity> _userManager { get; }

            public Handler(UserManager<UserEntity> userManager)
            {
                _userManager = userManager;
            }

            public override async Task<Response<Unit>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.Model.UserId);

                if (user == null)
                {
                    throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
                }

                // Ensure that the user is not null before changing the password
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.Model.OldPassword, request.Model.Password);

                if (!changePasswordResult.Succeeded)
                {
                    // Handle password change failure
                    throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Failed to change password");
                }

                return Success(new Unit());
            }
        }
    }
}

