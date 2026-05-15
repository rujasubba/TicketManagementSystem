using TicketManagementSystem.DTOs;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Interfaces
{
    public interface IUserDetailsService
    {
        Task<UserDetailsFormDto> GetFormDataAsync();
        Task<UserDetailsDto> CreateAsync(UserDetailsDto dto);
        Task<List<UserDetailsDto>> GetAllAsync();
        Task<UserDetailsDto> GetByIdAsync(int id);
    }
}
