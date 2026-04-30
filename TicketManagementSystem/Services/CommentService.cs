using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.DTOs.Comment;
using TicketManagementSystem.Interfaces;
using TicketManagementSystem.Models;
using TicketManagementSystem.Persistent;

namespace TicketManagementSystem.Services
{
    public class CommentService(AppDbContext dbContext, IAttachmentService attachmentService) : ICommentService
    {
        public async Task<CreateCommentDto> CreateAsync(CreateCommentDto model, string userId)
        {
            
            var ticket = await dbContext.Tickets
                .Include(t => t.TicketLogs)
            .FirstOrDefaultAsync(x => x.Id == model.TicketId);
            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }
            var isUserinvolved = ticket.TicketLogs.Any(tl => tl.AssignedUserId == userId);
            if (!isUserinvolved)
            {
                throw new Exception("User cannot comment on this ticket");
            }

            var attachmentList = new List<CommentAttachment>();

            if (model.Attachments != null && model.Attachments.Any())
            {
                foreach (var file in model.Attachments)
                {
                    var uploadedFile = await attachmentService.UploadFileAsync(file);

                    attachmentList.Add(new CommentAttachment
                    {
                        FileName = uploadedFile.FileName,
                        FilePath = uploadedFile.FilePath
                    });
                }
            }
            var comment = new Comment
            {
                Content = model.Content,
                CreatedByUserId = userId,
                CreatedDate = DateTime.Now,
                TicketId = model.TicketId,
                Attachments = attachmentList
                
            };
           await  dbContext.Comments.AddAsync(comment);
            await dbContext.SaveChangesAsync();
            model.SavedAttachments = attachmentList;
           
            return model;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CommentListDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
