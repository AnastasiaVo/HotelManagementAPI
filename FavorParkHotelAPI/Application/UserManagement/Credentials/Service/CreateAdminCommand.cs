using Hellang.Middleware.ProblemDetails;
using MediatR;
using FPH.Common;
using FPH.DataBase.Abstractions;
using Microsoft.AspNetCore.Identity;
using FPH.Data.Entities;
using Microsoft.EntityFrameworkCore;
using FPH.DataBase.Context;

namespace FavorParkHotelAPI.Application.UserManagement.Credentials.Service;

public class CreateAdminCommand : IRequest<Response<Unit>>
{
    public string Id { get; }

    public CreateAdminCommand(string id)
    {
        Id = id;
    }

    public class Handler : BaseHandler<CreateAdminCommand, Unit>
    {
        private const string AdminRole = "Admin";
        public UserManager<UserEntity> _userManager { get; }

        private readonly ApplicationDbContext _context;


        public Handler(UserManager<UserEntity> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _context = dbContext;
        }

        public override async Task<Response<Unit>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, AdminRole);
            if (!isAdmin)
            {
                var result = await _userManager.AddToRoleAsync(user, AdminRole);
                if (!result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, AdminRole);
                    await _context.SaveChangesAsync();
                }
            }
            return Success(new Unit());
        }
    }
}
