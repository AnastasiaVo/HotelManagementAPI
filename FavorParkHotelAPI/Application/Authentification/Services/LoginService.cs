using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using FPH.Common;
using Hellang.Middleware.ProblemDetails;
using FPH.Data.Entities;
using FPH.Common.Options;
using System.Text;
using System.Security.Claims;
using MediatR;
using System.Data;
using FavorParkHotelAPI.Application.Authentification.Dto;

namespace FavorParkHotelAPI.Application.Authentification.Services
{
    public class LoginService : IRequest<Response<GetTokenDto>>
    {
        public LoginService(LogInDto loginModel)
        {
            LoginModel = loginModel;
        }

        public LogInDto LoginModel { get; }

        public class Handler : BaseHandler<LoginService, GetTokenDto>
        {
            private readonly UserManager<UserEntity> _userManager;
            private readonly RoleManager<RoleEntity> _roleManager;
            private readonly IConfiguration _configuration;

            public Handler(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager, IConfiguration configuration)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _configuration = configuration;
            }

            public override async Task<Response<GetTokenDto>> Handle(LoginService request, CancellationToken cancellationToken)
            {
                var model = request.LoginModel;

                var user = await _userManager.FindByEmailAsync(model.Email);
                var isValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);
                var isValid = user is not null && isValidPassword;
                if (!isValid)
                {
                    throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "Invalid login or password");
                }

                var roles = await _userManager.GetRolesAsync(user);

                var result = new GetTokenDto()
                {
                    Token = CreateToken(user, roles),
                };

                return Success(result);
            }

            private string CreateToken(UserEntity User, IEnumerable<string> roles)
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var authOptions = new AuthConfigurationOptions(_configuration);

                var key = authOptions.SecretKey;
                var claims = CreateClaims(User, roles);

                var tokenDescriptor = CreateSecurityTokenDescriptor(claims, authOptions, key);
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }

            private static IEnumerable<Claim> CreateClaims(UserEntity User, IEnumerable<string> roles)
            {
                return roles
                    .Select(role => new Claim(ClaimTypes.Role, role))
                    .Append(new Claim(ClaimTypes.Name, User.Id))
                    .Append(new Claim(ClaimTypes.NameIdentifier, User.UserName ?? User.Id));
            }

            private static SecurityTokenDescriptor CreateSecurityTokenDescriptor(
                IEnumerable<Claim> claims,
                AuthConfigurationOptions authOptions,
                string key)
            {
                var bytes = Encoding.UTF8.GetBytes(key);
                var securityKey = new SymmetricSecurityKey(bytes);
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                return new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Issuer = authOptions.Issuer,
                    Audience = authOptions.Audience,
                    Expires = DateTime.UtcNow.AddDays(authOptions.LifeTime),
                    SigningCredentials = credentials,
                };
            }
        }
    }

}
