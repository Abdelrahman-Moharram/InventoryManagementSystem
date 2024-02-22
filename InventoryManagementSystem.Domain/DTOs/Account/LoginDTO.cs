﻿using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Domain.DTOs.Account
{
    public class LoginDTO
    {
        [Required, MinLength(2)]
        public string Username { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }

    }
}
