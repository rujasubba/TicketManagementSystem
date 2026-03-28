using TicketManagementSystem.Models;

namespace TicketManagementSystem.Interfaces
{
    public interface IPriorityService
    {
        void CreateAsync(Priority priority);
        void GetAllAsync();
        public Priority GetByIdAsync(int id);
        void UpdateAsync(Priority priority);
        void DeleteAsync(int id);
    }
}
