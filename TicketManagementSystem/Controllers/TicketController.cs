using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using TicketManagementSystem.DTOs.Ticket;
using TicketManagementSystem.Interfaces;
using TicketManagementSystem.Models;
using TicketManagementSystem.Persistent;
using TicketManagementSystem.Services;

namespace TicketManagementSystem.Controllers
{
    [Authorize]
    public class TicketController(AppDbContext dbContext, ITicketService service) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTicketDto model)
        {
            if (model == null)
            {
                throw new Exception("Nothing entered");
            }

            var createdTicket = await service.CreateAsync(model);

            if (createdTicket == null)
            {
                ModelState.AddModelError("", "Failed to create ticket");
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}
