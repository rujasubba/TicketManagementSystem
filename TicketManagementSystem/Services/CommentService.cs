using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.DTOs.Comment;
using TicketManagementSystem.Interfaces;
using TicketManagementSystem.Models;
using TicketManagementSystem.Persistent;

namespace TicketManagementSystem.Services
{
    public class CommentService(AppDbContext dbContext) : ICommentService
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
            var comment = new Comment
            {
                Content = model.Content,
                CreatedByUserId = userId,
               
                CreatedDate = DateTime.Now,
                TicketId = model.TicketId
            };
           await  dbContext.Comments.AddAsync(comment);
            await dbContext.SaveChangesAsync();
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
