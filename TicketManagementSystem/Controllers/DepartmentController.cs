using Microsoft.AspNetCore.Mvc;

namespace TicketManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
