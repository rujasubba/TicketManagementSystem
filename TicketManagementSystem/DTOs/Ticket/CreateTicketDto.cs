using TicketManagementSystem.Models;

namespace TicketManagementSystem.DTOs.Ticket
{
    public class CreateTicketDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int Category { get; set; }
        public int Status { get; set; } 
        public string CreatedByUser { get; set; }
        public string? AssignedUser { get; set; }
        public int Department { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
