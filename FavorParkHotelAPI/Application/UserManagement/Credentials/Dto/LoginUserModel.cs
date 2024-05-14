using System.ComponentModel.DataAnnotations;

namespace FavorParkHotelAPI.Application.UserManagement.Credentials.Dto
{
    public class LoginUserModel
    {
        [Required]
        public string Email { get; set; }
    }
}
