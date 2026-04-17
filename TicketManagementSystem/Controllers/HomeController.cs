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
            return View(ticketList);
        }

        public async Task<IActionResult>GetTicketByStatusId(int id)
        {
           
            var tickets = await ticketService.GetAllByStatusId(id, UserId);
            return View(tickets);
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
