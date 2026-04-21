using TicketManagementSystem.DTOs.Ticket;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Interfaces
{
    public interface ITicketService
    {
    
            Task<List<TicketListDto>> GetAllAsync();
        Task<List<TicketListDto>> GetAllAsyncByUserId(string id);
        
        Task<List<TicketListDto>> GetAllByStatusId(int id, string userId);
        Task<List<TicketListDto>> GetRecentAsyncByPriority(string userId);
        Task<List<TicketListDto>> GetRecentAsyncByUserId(string id);
        Task<Ticket> GetByIdAsync(int id);
        Task<Ticket> CreateAsync(CreateTicketDto model);
        Task<Ticket> UpdateAsync(CreateTicketDto model);
        Task DeleteAsync(int id);
     
    }
}
