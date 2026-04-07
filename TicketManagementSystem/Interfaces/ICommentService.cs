using TicketManagementSystem.DTOs.Comment;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentListDto>> GetAllAsync();
        Task<Comment> GetByIdAsync(int id);
        Task<CreateCommentDto> CreateAsync(CreateCommentDto model, string userId);
        Task<Comment> UpdateAsync();
        Task DeleteAsync(int id);
    }
}
