using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.DTOs.Ticket;
using TicketManagementSystem.Interfaces;
using TicketManagementSystem.Models;
using TicketManagementSystem.Persistent;

namespace TicketManagementSystem.Controllers
{
    [Authorize]
    public class TicketController(AppDbContext dbContext, ITicketService service) : BaseController
    {

        public async Task<IActionResult> Index([FromQuery] TicketFilterDto filter)
        {
            ViewBag.CurrentUser = UserId;
            ViewBag.Priorities = dbContext.Priorities.ToList();
            ViewBag.Statuses = dbContext.Status.ToList();
            ViewBag.Categories = dbContext.Categories.ToList();
            ViewBag.Departments = dbContext.Departments.ToList();
            ViewBag.Filter = filter;

            var ticketList = await service.GetFilteredAsync(UserId, filter);
            return View(ticketList);
        }

        [HttpGet]
        public IActionResult Create(CreateTicketDto model)
        {
            ViewBag.Priorities = dbContext.Priorities.ToList();
            ViewBag.Statuses = dbContext.Status.ToList();
            ViewBag.Categories = dbContext.Categories.ToList();
            ViewBag.Departments = dbContext.Departments.ToList();
            ViewBag.Users = dbContext.Users.ToList();
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateTicketDto model)
        {
            if (model == null)
            {
                throw new Exception("Nothing entered");
            }

            model.CreatedByUser = UserId;

            var createdTicket = await service.CreateAsync(model);


            if (createdTicket == null)
            {
                ModelState.AddModelError("", "Failed to create ticket");
                return PartialView(model);
            }

            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Details(int id)
        {
            var ticket = await service.GetByIdAsync(id);
            if (ticket == null) return NotFound();
            return View(ticket);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await service.GetByIdAsync(id);
            if(ticket == null)
            {
                throw new Exception("Ticket not found");
            }
            var model = new CreateTicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.PriorityId,
                Category = ticket.CategoryId,
                Status = ticket.StatusId,
                Department = ticket.DepartmentId,
                CreatedByUser = ticket.CreatedByUserId,
                CreatedDate = ticket.CreatedDate,
                AssignedUser = ticket.AssignedToUserId,
                ExistingAttachments = ticket.Attachments?.Select(a => new ExistingAttachmentDto
                {
                    Id = a.Id,
                    AttachmentName = a.AttachmentName,
                    AttachmentPath = a.AttachmentPath
                }).ToList()
            };

            ViewBag.Priorities = dbContext.Priorities.ToList();
            ViewBag.Statuses = dbContext.Status.ToList();
            ViewBag.Categories = dbContext.Categories.ToList();
            ViewBag.Departments = dbContext.Departments.ToList();
            ViewBag.Users = dbContext.Users.ToList();
            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateTicketDto model)
        {
            model.CreatedByUser = UserId;
            ModelState.Remove(nameof(model.CreatedByUser));
            if (!ModelState.IsValid)
            {
                ViewBag.Priorities = dbContext.Priorities.ToList();
                ViewBag.Statuses = dbContext.Status.ToList();
                ViewBag.Categories = dbContext.Categories.ToList();
                ViewBag.Departments = dbContext.Departments.ToList();
                ViewBag.Users = dbContext.Users.ToList();

                foreach (var item in ModelState)
                {
                    foreach (var error in item.Value.Errors)
                    {
                        Console.WriteLine($"{item.Key}: {error.ErrorMessage}");
                    }
                }

                return View("Create", model);
            }
                

            await service.UpdateAsync(model);

            return RedirectToAction("Index");
        }

       

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await service.GetByIdAsync(id);
            if(ticket == null)
            {
                return NotFound();
            }
            return PartialView(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
