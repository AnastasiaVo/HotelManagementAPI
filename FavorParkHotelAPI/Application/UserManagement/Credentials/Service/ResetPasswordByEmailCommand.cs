using Hellang.Middleware.ProblemDetails;
using MediatR;
using FPH.Common;
using FavorParkHotelAPI.Application.UserManagement.Credentials.Dto;
using FPH.Data.Entities;
using Microsoft.AspNetCore.Identity;
using FPH.DataBase.Context;

namespace FavorParkHotelAPI.Application.UserManagement.Credentials.Service;

public class ResetPasswordByEmailCommand : IRequest<Response<Unit>>
{
    public ResetPasswordModelByEmail Model { get; }

    public ResetPasswordByEmailCommand(ResetPasswordModelByEmail model)
    {
        Model = model;
    }

    public class Handler : BaseHandler<ResetPasswordByEmailCommand, Unit>
    {
        public UserManager<UserEntity> _userManager { get; }
        private readonly ApplicationDbContext _context;

        public Handler(UserManager<UserEntity> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _context = dbContext;
        }

        public override async Task<Response<Unit>> Handle(ResetPasswordByEmailCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && user.TempPassword == model.TempPassword && user.TempPassword != null)
            {
                user.TempPassword = null;
                var removeResult = await _userManager.RemovePasswordAsync(user);
                if (removeResult.Succeeded)
                {
                    var addResult = await _userManager.AddPasswordAsync(user, model.Password);
                    if (addResult.Succeeded)
                    {
                        //await _userManager.UpdateAsync(user); // Update the user entity
                        await _context.SaveChangesAsync();
                        return Success(new Unit());
                    }
                }
            }

            throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Invalid password or login");
        }
    }
}
