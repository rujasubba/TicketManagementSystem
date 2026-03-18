using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.Models;
using TicketManagementSystem.Persistent;
using TicketManagementSystem.Services;

namespace TicketManagementSystem.Controllers
{
    public class UserController(AppDbContext dbContext, UserService service) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            await service.AddUser(user);
            return View();
        }
    }
}
