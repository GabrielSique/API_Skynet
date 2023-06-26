using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Models.Dtos
{
    public class AuthenticatorDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public string nameUser { get; set; }
        public string rolName { get; set; }
    }
}
