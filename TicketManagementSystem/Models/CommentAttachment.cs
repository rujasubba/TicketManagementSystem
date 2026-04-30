namespace TicketManagementSystem.Models
{
    public class CommentAttachment
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
