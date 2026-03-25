using TicketManagementSystem.DTOs.Ticket;
using TicketManagementSystem.Interfaces;
using TicketManagementSystem.Models;
using TicketManagementSystem.Persistent;

namespace TicketManagementSystem.Services
{
    public class TicketService(AppDbContext dbContext) : ITicketService
    {
        public async Task<Ticket> CreateAsync(CreateTicketDto model)
        {
            if (model == null)
            {
                throw new Exception("Model cannot be null");
            }
            var ticket = new Ticket
            {
                TicketNo = $"TCKT-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                Title = model.Title,
                Description = model.Description,
                PriorityId = model.Priority,
                CategoryId = model.Category,
                StatusId = model.Status,
                CreatedByUserId = model.CreatedByUser,
                AssignedToUserId = model.AssignedUser,
                DepartmentId = model.Department,
                CreatedDate = DateTime.UtcNow
            };
            await dbContext.Tickets.AddAsync(ticket);
            await dbContext.SaveChangesAsync();
            return ticket;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }

    }
}
