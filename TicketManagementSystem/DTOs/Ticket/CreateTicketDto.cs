using System.ComponentModel.DataAnnotations;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.DTOs.Ticket
{
    public class CreateTicketDto
    {
        [Required(ErrorMessage ="Title is Required"), MaxLength(150)]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Please select Priority")]
        public int Priority { get; set; }
        [Required(ErrorMessage = "Please select Category")]
        public int Category { get; set; }
        [Required(ErrorMessage = "Please select Status")]
        public int Status { get; set; } 
        public string CreatedByUser { get; set; }
        public string? AssignedUser { get; set; }
        [Required(ErrorMessage = "Please select Department")]
        public int Department { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
