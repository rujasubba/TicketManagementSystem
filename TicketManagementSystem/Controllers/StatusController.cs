using Microsoft.AspNetCore.Mvc;

namespace TicketManagementSystem.Controllers
{
    public class StatusController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
