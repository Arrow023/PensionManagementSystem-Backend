﻿using System.ComponentModel.DataAnnotations;

namespace PensionManagementSystem.Models
{
    public class Authentication
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
