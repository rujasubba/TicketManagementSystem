using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.DTOs.Category;
using TicketManagementSystem.DTOs.Department;
using TicketManagementSystem.Interfaces;
using TicketManagementSystem.Models;
using TicketManagementSystem.Persistent;

namespace TicketManagementSystem.Services
{
    public class DepartmentService(AppDbContext dbContext) : IDepartmentService
    {
        public async Task<Department> CreateAsync(CreateDepartmentDto model)
        {
           if(model == null)
            {
                throw new Exception("Model cannot be null");
            }
            var name = await dbContext.Departments.FirstOrDefaultAsync(x => x.Name == model.Name);
            if(name != null)
            {
                throw new Exception(" This name already exists");
            }
            var department = new Department
            {
                Name = model.Name
            };
           await dbContext.Departments.AddAsync(department);
            await dbContext.SaveChangesAsync();
            return department;
        }

        public async Task DeleteAsync(int id)
        {
            if (id == 0)
            {
                throw new Exception("Id must not be Zero");
            }
            var department = await dbContext.Departments.FindAsync(id);

            if (department == null)
            {
                throw new Exception("Department with this Id not found");
            }

            dbContext.Departments.Remove(department);
            await dbContext.SaveChangesAsync();
        }
        

        public async Task<List<DepartmentListDto>> GetAllAsync()
        {
          var departments = await dbContext.Departments
                .Select(d => new DepartmentListDto
                {
                    DepartmentId = d.DepartmentId,
                    Name = d.Name,
                })
                .ToListAsync();
            return departments;

        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await dbContext.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);
            
        }

        public Task<Department> UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }
        
    }
}
