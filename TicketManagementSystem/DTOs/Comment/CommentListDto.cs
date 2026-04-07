using TicketManagementSystem.Models;

namespace TicketManagementSystem.DTOs.Comment
{
    public class CommentListDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserId { get; set; }
        public int TicketId { get; set; }
    }
}
