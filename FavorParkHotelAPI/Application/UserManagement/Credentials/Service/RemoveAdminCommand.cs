using Hellang.Middleware.ProblemDetails;
using MediatR;
using FPH.Common;
using Microsoft.AspNetCore.Identity;
using FPH.Data.Entities;
using FPH.DataBase.Context;
using FavorParkHotelAPI;

namespace FavorParkHotelAPI.Application.UserManagement.Credentials.Service;

public class RemoveAdminCommand : IRequest<Response<Unit>>
{
    public string Id { get; }

    public RemoveAdminCommand(string id)
    {
        Id = id;
    }

    public class Handler : BaseHandler<RemoveAdminCommand, Unit>
    {
        private const string AdminRole = "Admin";
        public UserManager<UserEntity> _userManager { get; }
        private readonly ApplicationDbContext _context;

        public Handler(UserManager<UserEntity> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _context = dbContext;
        }

        public override async Task<Response<Unit>> Handle(RemoveAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                throw new ProblemDetailsException(StatusCodes.Status404NotFound, "User not found");
            }

            if (await _userManager.IsInRoleAsync(user, AdminRole))
            {
                await _userManager.RemoveFromRoleAsync(user, AdminRole);
                await _context.SaveChangesAsync();
            }

            return Success(new Unit());
        }
    }
}
