using TicketManagementSystem.DTOs.Ticket;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Interfaces
{
    public interface ITicketLogService
    {
        //Task<List<TicketLogDto>> GetAllAsync();
        Task<TicketLog> GetByIdAsync(int id);
        Task<TicketLog> CreateAsync();
        Task<TicketLog> UpdateAsync();
        Task DeleteAsync(int id);
    }
}
