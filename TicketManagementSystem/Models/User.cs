using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TicketManagementSystem.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        public string FullName { get; set; }

        public string Address { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }
        public IEnumerable<TicketLog> TicketLogs { get; set; }
    }
}
