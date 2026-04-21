using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using TicketManagementSystem.DTOs.Ticket;
using TicketManagementSystem.Interfaces;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Controllers
{
    [Authorize]
    public class HomeController(ITicketService ticketService) : BaseController
    {
        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentUser = UserId;
            
            var ticketList = await ticketService.GetAllAsyncByUserId(UserId);
            var recentRequests = await ticketService.GetRecentAsyncByUserId(UserId);
            var ticketsByPriority = await ticketService.GetRecentAsyncByPriority(UserId);

            ViewBag.RecentRequests = recentRequests;
            ViewBag.TicketsByPriority = ticketsByPriority;
            return View(ticketList);
        }

        public async Task<IActionResult>GetTicketByStatusId(int id)
        {
            ViewBag.CurrentUser = UserId;
            var tickets = await ticketService.GetAllByStatusId(id, UserId);
            return View(tickets);
        }

        public async Task<IActionResult> GetRecentTicketsByUserId()
        {

            var tickets = await ticketService.GetRecentAsyncByUserId(UserId);
            return View("Index", tickets);
            
        }

        public async Task<IActionResult> GetRecentTicketByPriority()
        {
            var tickets = await ticketService.GetRecentAsyncByPriority(UserId);
            return View("Index", tickets);
        }

        [HttpGet]
        public async Task<IActionResult> GetTicketStatusChart()
        {
            var tickets = await ticketService.GetAllAsyncByUserId(UserId);

            var result = new
            {
                open = tickets.Count(t => t.Status == "Open"),
                inProgress = tickets.Count(t => t.Status == "In Progress"),
                closed = tickets.Count(t => t.Status == "Closed")
            };

            return Json(result);
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
