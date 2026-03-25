using TicketManagementSystem.DTOs.Ticket;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Interfaces
{
    public interface ITicketService
    {
    
            Task<List<Ticket>> GetAllAsync();
            Task<Ticket> GetByIdAsync(int id);
            Task<Ticket> CreateAsync(CreateTicketDto model);
            Task<bool> UpdateAsync(Ticket ticket);
            Task<bool> DeleteAsync(int id);
     
    }
}
