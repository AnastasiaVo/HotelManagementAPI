using FPH.Data.Entities;

namespace FavorParkHotelAPI.Application.Authentification.Dto
{
    public class GetUserDto
    {
        public GetUserDto(UserEntity user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;

        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
