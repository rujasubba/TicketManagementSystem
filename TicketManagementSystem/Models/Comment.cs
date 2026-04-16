namespace TicketManagementSystem.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public User CreatedByUser { get; set; }
        public string CreatedByUserId { get; set; }
       
        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
    }
}
