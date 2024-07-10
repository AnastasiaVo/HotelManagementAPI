using Microsoft.AspNetCore.Identity;
using FPH.Common;
using Hellang.Middleware.ProblemDetails;
using FPH.Data.Entities;
using FPH.Common.Options;
using MediatR;
using FavorParkHotelAPI.Application.Authentification.Dto;

namespace FavorParkHotelAPI.Application.Authentification.Services
{
    
    public class CreateUserService : IRequest<Response<GetUserDto>>
    {
        public CreateUserService(RegisterUserDto userApplyModel)
        {
            UserApplyModel = userApplyModel;
        }

        public RegisterUserDto UserApplyModel { get; }

        public class Handler : BaseHandler<CreateUserService, GetUserDto>
        {
            private const string DefaultUserRole = "User";
            private const string AdminRole = "Admin";

            private readonly UserManager<UserEntity> _userManager;
            private readonly RoleManager<RoleEntity> _roleManager;
            private readonly IConfiguration _configuration;

            public Handler(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager, IConfiguration configuration)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _configuration = configuration;
            }

            public override async Task<Response<GetUserDto>> Handle(CreateUserService request, CancellationToken cancellationToken)
            {
                var jwtOptions = new ConnectionStringOptions(_configuration);
                var model = request.UserApplyModel;

                var user = new UserEntity()
                {
                    Email = model.Email,
                    UserName = model.FirstName + model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var roles = new List<string> { DefaultUserRole, AdminRole };

                var identityResult = await _userManager.CreateAsync(user, model.Password);
                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, roles);
                }
                else
                {
                    throw new ProblemDetailsException(StatusCodes.Status400BadRequest, identityResult.Errors.FirstOrDefault()?.Description ?? "Something went wrong");
                }

                var result = new GetUserDto(user);

                return Success(result);
            }
        }
    }

}

