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

            if (model.CommentContents != null && model.CommentContents.Any())
            {
                foreach (var comment in model.CommentContents)
                {
                    if (!string.IsNullOrWhiteSpace(comment))
                    {
                        commentList.Add(new Comment
                        {
                            Content = comment,
                            CreatedByUserId = model.CreatedByUser,
                            CreatedDate = DateTime.Now
                        });
                    }
                }
            }
            var ticketLogList = new List<TicketLog>();
            if(model.AssignedUser != null)
            {
                ticketLogList.Add(new TicketLog
                {
                    AssignedUserId = model.CreatedByUser,
                    AssignedDate = DateTime.Now,
                    IsActive = false

                });
                ticketLogList.Add(new TicketLog
                {
                    AssignedUserId = model.AssignedUser,
                    AssignedDate = DateTime.Now,
                    IsActive = true

                });

            }
            else
            {
                ticketLogList.Add(new TicketLog
                {
                    AssignedUserId = model.CreatedByUser,
                    AssignedDate = DateTime.Now,
                    IsActive = true

                });
            }

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
                Comments = commentList,
                TicketLogs = ticketLogList

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
        .Include(t => t.TicketLogs)
        .ThenInclude(tl => tl.AssignedUser)
        .FirstOrDefaultAsync(t => t.Id == id);
      
        }


        public async Task<Ticket> UpdateAsync(CreateTicketDto dto)
        {
  
            var ticket = await dbContext.Tickets
            .Include(t => t.Comments)
            .Include(t => t.TicketLogs)
            .FirstOrDefaultAsync(t => t.Id == dto.Id);

            if (ticket == null)
            {
                throw new Exception("Ticket not found");
            }


            if (!string.IsNullOrEmpty(dto.AssignedUser))
            {
                var user = await dbContext.Users
                    .FirstOrDefaultAsync(u => u.Id == dto.AssignedUser);

                if (user != null && !string.IsNullOrEmpty(user.Email))
                {
                    string subject = $"Ticket Assigned - {ticket.TicketNo}";

                    string body = $@"
                   <h3>New Ticket Assigned</h3>
                   <p>Hi {user.FullName},</p>

                    <p>You have been assigned a new ticket.</p>

                    <p><b>Title:</b> {ticket.Title}</p>
                    <p><b>Description:</b> {ticket.Description}</p>
                    <p><b>Priority:</b> {ticket.PriorityId}</p>

                      <p>Please check and take action.</p>
                     ";

                    EmailHelper.SendEmail(user.Email, subject, body);
                }
            }



            var commentList = ticket.Comments.ToList();
            if (dto.CommentContents != null && dto.CommentContents.Any())
            {
                foreach (var comment in dto.CommentContents)
                {
                    if (!string.IsNullOrWhiteSpace(comment))
                    {
                        commentList.Add(new Comment
                        {
                            Content = comment,
                            CreatedByUserId = dto.CreatedByUser,
                            CreatedDate = DateTime.Now
                        });
                    }
                }
            }

           
            var currentActiveLog = ticket.TicketLogs
                .FirstOrDefault(x => x.IsActive);
            if (dto.AssignedUser != ticket.AssignedToUserId)
            {
                if (currentActiveLog != null)
                {
                    currentActiveLog.IsActive = false;
                }
                if (!string.IsNullOrEmpty(dto.AssignedUser))
                {
                    ticket.TicketLogs.Add(new TicketLog
                    {
                        AssignedUserId = dto.AssignedUser,
                        AssignedDate = DateTime.Now,
                        IsActive = true
                    });
                }
            }

            ticket.Title = dto.Title;
            ticket.Description = dto.Description;
            ticket.PriorityId = dto.Priority;
            ticket.CategoryId = dto.Category;
            ticket.StatusId = dto.Status;
            ticket.DepartmentId = dto.Department;
            ticket.AssignedToUserId = dto.AssignedUser;
            ticket.Comments = commentList;

            await dbContext.SaveChangesAsync();

            return ticket;
        }
    }
}
