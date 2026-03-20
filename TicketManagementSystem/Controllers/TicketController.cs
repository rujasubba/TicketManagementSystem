using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketManagementSystem.Persistent;

namespace TicketManagementSystem.Controllers
{
    [Authorize]
    public class TicketController(AppDbContext dbContext) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
