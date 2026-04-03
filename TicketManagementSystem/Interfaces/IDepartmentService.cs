using TicketManagementSystem.DTOs.Category;
using TicketManagementSystem.DTOs.Department;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentListDto>> GetAllAsync();
        Task<Department> GetByIdAsync(int id);
        Task<Department> CreateAsync(CreateDepartmentDto model);
        Task<Department> UpdateAsync(int id);
        Task DeleteAsync(int id);
    }
}
