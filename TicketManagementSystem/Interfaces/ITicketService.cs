using TicketManagementSystem.DTOs.Ticket;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Interfaces
{
    public interface ITicketService
    {
    
            Task<List<TicketListDto>> GetAllAsync();
            Task<Ticket> GetByIdAsync(int id);
        Task<Ticket> CreateAsync(CreateTicketDto model);
        Task<Ticket> UpdateAsync(UpdateTicketDto model);
        Task DeleteAsync(int id);
     
    }
}
