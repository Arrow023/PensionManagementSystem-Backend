using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionManagementSystem.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string? Token { get; set; }
        [NotMapped]
        public int? ExpiresIn { get; set; }
    }
}
