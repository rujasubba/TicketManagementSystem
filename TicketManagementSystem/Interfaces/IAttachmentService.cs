using TicketManagementSystem.DTOs.Attachment;

namespace TicketManagementSystem.Interfaces
{
    public interface IAttachmentService
    {
        Task<AttachmentDto> UploadFileAsync(IFormFile file);
    }
}
