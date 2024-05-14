using FPH.Common;
using MediatR;
using FPH.DataBase.Abstractions;
using Hellang.Middleware.ProblemDetails;
using FavorParkHotelAPI.Application.UserManagement.Dto;

namespace FavorParkHotelAPI.Application.UserManagement.Services
{
    public class UpdateUserService : IRequest<Response<UserDto>>
    {
        public UpdateUserService(string userId, UpdateUserDto updateUserDto)
        {
            UserId = userId;
            UpdateUserDto = updateUserDto;
        }

        public string UserId { get; }
        public UpdateUserDto UpdateUserDto { get; }
    }

    public class UpdateUserServiceHandler : BaseHandler<UpdateUserService, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserServiceHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<Response<UserDto>> Handle(UpdateUserService request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            var updateUserDto = request.UpdateUserDto;

            var userEntity = await _userRepository.GetUserByIdAsync(userId);
            if (userEntity == null)
                throw new ProblemDetailsException(StatusCodes.Status400BadRequest, "User not found.");

            // Update the entity with the new data
            userEntity.FirstName = updateUserDto.FirstName;
            userEntity.LastName = updateUserDto.LastName;
            userEntity.PhoneNumber = updateUserDto.PhoneNumber;

            await _userRepository.UpdateAsync(userEntity);

            return Success(new UserDto
            {
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                PhoneNumber = userEntity.PhoneNumber
            });
        }
    }
}