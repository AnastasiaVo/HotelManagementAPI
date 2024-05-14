using Hellang.Middleware.ProblemDetails;
using MediatR;
using FPH.Common;
using Microsoft.AspNetCore.Identity;
using FPH.Data.Entities;
using FPH.DataBase.Context;

namespace FavorParkHotelAPI.Application.UserManagement.Credentials.Service
{
    public class DropPasswordCommand : IRequest<Response<int>>
    {
        public string Email { get; }

        public DropPasswordCommand(string email)
        {
            Email = email;
        }

        public class Handler : BaseHandler<DropPasswordCommand, int>
        {
            public UserManager<UserEntity> _userManager { get; }
            private readonly ApplicationDbContext _context;

            public Handler(UserManager<UserEntity> userManager, ApplicationDbContext dbContext)
            {
                _userManager = userManager;
                _context = dbContext;
            }

            public override async Task<Response<int>> Handle(DropPasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
                }

                var password = new Random().Next(1000, 1000000);
                user.TempPassword = password;

                //await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();

                return Success(password);
            }
        }
    }
}

