using Hellang.Middleware.ProblemDetails;
using MediatR;
using FPH.Common;
using FavorParkHotelAPI.Application.UserManagement.Credentials.Dto;
using FPH.Data.Entities;
using FavorParkHotelAPI;
using Microsoft.AspNetCore.Identity;
using FPH.DataBase.Context;

namespace FavorParkHotelAPI.Application.UserManagement.Credentials.Service;

public class UpdateUserLoginCommand : IRequest<Response<Unit>>
{
    public string LoggedInUserId { get; }
    public LoginUserModel LoginUserModel { get; }

    public UpdateUserLoginCommand(string loggedInUserId, LoginUserModel loginUserModel)
    {
        LoggedInUserId = loggedInUserId;
        LoginUserModel = loginUserModel;
    }
    public class Handler : BaseHandler<UpdateUserLoginCommand, Unit>
    {
        public UserManager<UserEntity> _userManager { get; }
        private readonly ApplicationDbContext _context;

        public Handler(UserManager<UserEntity> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _context = dbContext;
        }

        public override async Task<Response<Unit>> Handle(UpdateUserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.LoggedInUserId);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
            }

            user.Email = request.LoginUserModel.Email;

            await _context.SaveChangesAsync();

            return Success(new Unit());
        }
    }
}
