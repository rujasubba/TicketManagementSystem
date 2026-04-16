using TicketManagementSystem.DTOs.Comment;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentListDto>> GetAllAsync();
       
        Task<CreateCommentDto> CreateAsync(CreateCommentDto model, string userId);
     
    }
}
