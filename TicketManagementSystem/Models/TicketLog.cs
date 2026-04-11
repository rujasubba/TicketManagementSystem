namespace TicketManagementSystem.Models
{
    public class TicketLog
    {
        public int Id { get; set; }
        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
        public DateTime AssignedDate { get; set; }
        public User AssignedUser { get; set; }
        public string AssignedUserId { get; set; }
        public bool IsActive { get; set; }
        

    }
}
