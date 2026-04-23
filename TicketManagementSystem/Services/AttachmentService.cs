using TicketManagementSystem.DTOs.Attachment;
using TicketManagementSystem.Interfaces;

namespace TicketManagementSystem.Services
{
    public class AttachmentService : IAttachmentService
    {
        public async Task<AttachmentDto> UploadFileAsync(IFormFile file)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid() + "_" + file.FileName;
            var fullPath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return new AttachmentDto
            {
                FileName = fileName,
                FilePath = "/uploads/" + fileName
            };
        }
    }
}
