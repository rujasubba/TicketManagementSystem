using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.DTOs.Comment;
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
            var commentList = new List<Comment>();

            commentList.Add(new Comment
            {
                Content = model.CommentContent,
                CreatedByUserId = model.CreatedByUser,
                CreatedDate = DateTime.Now
            });
     

            var ticket = new Ticket
            {
                TicketNo = $"TCKT-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                Title = model.Title,
                Description = model.Description,
                PriorityId = model.Priority,
                CategoryId = model.Category,
                StatusId = 1,
                CreatedByUserId = model.CreatedByUser,
                AssignedToUserId = model.AssignedUser,
                DepartmentId = model.Department,
                CreatedDate = DateTime.Now,
                Comments = commentList
                
            };
            var ticketNo = await dbContext.Tickets.FirstOrDefaultAsync(x => x.TicketNo == ticket.TicketNo);
            if(ticketNo != null)
            {
                throw new Exception("Ticket No. should not be duplicated");
            }
            await dbContext.Tickets.AddAsync(ticket);
            await dbContext.SaveChangesAsync();
            return ticket;
        }

        public async Task DeleteAsync(int id)
        {
            if(id == 0)
            {
                throw new Exception("Id must not be Zero");
            }
            var ticket = await dbContext.Tickets.FindAsync(id);

            if (ticket == null)
            { throw new Exception("Ticket with this Id not found"); 
            }

            dbContext.Tickets.Remove(ticket);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<TicketListDto>> GetAllAsync()
        {
            var tickets = await dbContext.Tickets
                .Include(t => t.Priority)
                .Include(t => t.Category)
                .Include(t => t.Status)
                .Include(t => t.Department)
                .Include(t => t.CreatedByUser)
                .Select(t => new TicketListDto
                {
                    Id = t.Id,
                    TicketNo = t.TicketNo,
                    Title = t.Title,
                    Description = t.Description,
                    Priority = t.Priority.Name,
                    Category = t.Category.Name,
                    Status = t.Status.Name,
                    CreatedByUserId = t.CreatedByUserId,
                    AssignedToUserId = t.AssignedToUserId,
                    Department = t.Department.Name,
                    CreatedDate = t.CreatedDate
                })
                .ToListAsync();
            return tickets;

        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
        return await dbContext.Tickets
        .Include(t => t.Priority)
        .Include(t => t.Category)
        .Include(t => t.Status)
        .Include(t => t.Department)
        .Include(t => t.Comments)
        .Include(t => t.AssigenedUser)
        .Include(t => t.CreatedByUser)
        .FirstOrDefaultAsync(t => t.Id == id);
      
        }


        public async Task<Ticket> UpdateAsync(UpdateTicketDto dto)
        {
            var ticket = await dbContext.Tickets.FindAsync(dto.Id);

            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }


            ticket.Title = dto.Title;
            ticket.Description = dto.Description;
            ticket.PriorityId = dto.PriorityId;
            ticket.CategoryId = dto.CategoryId;
            ticket.StatusId = dto.StatusId;
            ticket.DepartmentId = dto.DepartmentId;

            await dbContext.SaveChangesAsync();

            return ticket;
        }
    }
}
