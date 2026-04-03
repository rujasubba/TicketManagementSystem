using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using TicketManagementSystem.Interfaces;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Controllers
{
    [Authorize]
    public class HomeController(ITicketService ticketService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var ticketList = await ticketService.GetAllAsync();
            return View(ticketList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
