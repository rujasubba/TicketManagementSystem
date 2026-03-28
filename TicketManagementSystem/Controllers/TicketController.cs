using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.DTOs.Ticket;
using TicketManagementSystem.Interfaces;
using TicketManagementSystem.Persistent;

namespace TicketManagementSystem.Controllers
{
    [Authorize]
    public class TicketController(AppDbContext dbContext, ITicketService service) : BaseController
    {
        public async Task <IActionResult> Index()
        {
            var ticketList = await service.GetAllAsync();

            return View(ticketList);
        }



        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Priorities = dbContext.Priorities.ToList();
            ViewBag.Statuses = dbContext.Status.ToList();
            ViewBag.Categories = dbContext.Categories.ToList();
            ViewBag.Departments = dbContext.Departments.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketDto model)
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
                return View(model);
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await service.GetByIdAsync(id);
            if (ticket == null)
                return NotFound();
            return View(ticket);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await service.GetByIdAsync(id);
            if(ticket == null)
            {
                throw new Exception("Ticket not found");
            }
            var model = new UpdateTicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                PriorityId = ticket.PriorityId,
                CategoryId = ticket.CategoryId,
                StatusId = ticket.StatusId,
                DepartmentId = ticket.DepartmentId,
               
            };
            ViewBag.Priorities = dbContext.Priorities.ToList();
            ViewBag.Statuses = dbContext.Status.ToList();
            ViewBag.Categories = dbContext.Categories.ToList();
            ViewBag.Departments = dbContext.Departments.ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateTicketDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var updated = await service.UpdateAsync(model);

            if (updated == null)
                return NotFound();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
