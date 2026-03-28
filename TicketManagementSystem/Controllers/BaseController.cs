using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TicketManagementSystem.Controllers
{
    public class BaseController : Controller
    {
        protected string? UserId => 
            User?.FindFirstValue(ClaimTypes.NameIdentifier);
        protected bool IsLoggedIn =>
            User?.Identity?.IsAuthenticated ?? false;
    }
}
