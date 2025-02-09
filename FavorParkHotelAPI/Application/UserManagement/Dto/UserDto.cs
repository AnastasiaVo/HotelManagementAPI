﻿using System.ComponentModel.DataAnnotations;

namespace FavorParkHotelAPI.Application.UserManagement.Dto
{
    public class UserDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public bool Admin { get; set; }

    }
}