using TicketManagementSystem.Models;

namespace TicketManagementSystem.DTOs.Comment
{
    public class CreateCommentDto
    {
        public string Content { get; set; }
        public int TicketId { get; set; }
        public List<IFormFile>? Attachments { get; set; }
        public List<CommentAttachment>? SavedAttachments { get; set; }
    }

    public class CommentAttachmentDto
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
