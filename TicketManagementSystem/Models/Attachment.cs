namespace TicketManagementSystem.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentPath { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
