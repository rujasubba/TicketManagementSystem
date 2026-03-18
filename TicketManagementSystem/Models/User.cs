using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TicketManagementSystem.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public string Name {get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }
        public bool IsActive { get; set; }

    }
}
