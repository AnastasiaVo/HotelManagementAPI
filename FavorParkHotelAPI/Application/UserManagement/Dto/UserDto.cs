using System.ComponentModel.DataAnnotations;

namespace FavorParkHotelAPI.Application.UserManagement.Dto
{
    public class UserDto
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        public bool Admin { get; set; }

    }
}