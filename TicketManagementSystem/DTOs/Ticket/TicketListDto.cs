using TicketManagementSystem.Models;

namespace TicketManagementSystem.DTOs.Ticket
{
    public class TicketListDto
    {
        public int Id { get; set; }
        public string TicketNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority{ get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedByFullName { get; set; }
        public string? AssignedToUserId { get; set; }
        public string AssignedToFullName { get; set; }
        public string Department { get; set; }
        public DateTime CreatedDate { get; set; }
       

    }
}
