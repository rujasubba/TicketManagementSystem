using TicketManagementSystem.DTOs.Category;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Interfaces
{
    public interface IcategoryService
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(int id);
        Task<Category> Create(CreateCategoryDto dto);
        Task<Category> Update(int id, UpdateCategoryDto dto);
        Task<bool> Delete(int id);
    }
}
