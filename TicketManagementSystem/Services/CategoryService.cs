using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.DTOs.Category;
using TicketManagementSystem.Interfaces;
using TicketManagementSystem.Models;
using TicketManagementSystem.Persistent;

namespace TicketManagementSystem.Services
{
    public class CategoryService(AppDbContext dbContext) : IcategoryService
    {
        public async Task<List<Category>> GetAll()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await dbContext.Categories.FindAsync(id);
        }

        public async Task<Category> Create(CreateCategoryDto dto)
        {
            var name = await dbContext.Categories
                .FirstOrDefaultAsync(x => x.Name == dto.Name);
            if(name != null)
            {
                throw new Exception("This category already exists");
            }
            var category = new Category
            {
                Name = dto.Name
            };

            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<Category> Update(int id, UpdateCategoryDto dto)
        {
            var category = await dbContext.Categories.FindAsync(id);

            if (category == null)
                throw new Exception("Category not found");

            category.Name = dto.Name;

            await dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<bool> Delete(int id)
        {
            var category = await dbContext.Categories.FindAsync(id);

            if (category == null)
                return false;

            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
