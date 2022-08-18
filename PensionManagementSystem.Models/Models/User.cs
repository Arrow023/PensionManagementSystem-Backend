using System.ComponentModel.DataAnnotations.Schema;

namespace PensionManagementSystem.Models
{
    public class User
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
