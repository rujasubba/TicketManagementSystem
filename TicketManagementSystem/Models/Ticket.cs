namespace TicketManagementSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string TicketNo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public int PriorityId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Status Status { get; set; }
        public int StatusId { get; set; }
        public User CreatedByUser { get; set; }
        public string CreatedByUserId { get; set; }
        public User AssigenedUser { get; set;  }
        public string ?AssignedToUserId { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<TicketLog> TicketLogs { get; set; }

    }
}
