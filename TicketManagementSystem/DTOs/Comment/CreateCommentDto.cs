using TicketManagementSystem.Models;

namespace TicketManagementSystem.DTOs.Comment
{
    public class CreateCommentDto
    {
        public string Content { get; set; }
        public int TicketId { get; set; }
    }


}
