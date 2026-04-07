using TicketManagementSystem.Models;

namespace TicketManagementSystem.DTOs.Comment
{
    public class CreateCommentDto
    {
        public string Content { get; set; }
        public string CreatedByUserId { get; set; }
        public string CreatedByUserName { get; set; }
        public int TicketId { get; set; }
        //public List<Comment> Comments { get; set; }

    }

    //public class Comment
    //{
    //    public string Content { get; set; }
    //    public DateTime CreatedDate { get; set; }
    //    public string CreatedByUserId { get; set; }
    //    public string TicketId { get; set; }
    //}
}
