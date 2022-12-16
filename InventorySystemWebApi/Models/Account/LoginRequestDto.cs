﻿using System.ComponentModel.DataAnnotations;

namespace InventorySystemWebApi.Models.Account
{
    public class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}
