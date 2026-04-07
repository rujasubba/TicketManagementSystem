using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketManagementSystem.DTOs.Comment;
using TicketManagementSystem.Services;

namespace TicketManagementSystem.Controllers
{
    public class CommentController(CommentService service) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await service.CreateAsync(dto, userId!);

            return Json(new
            {
                success = true
            });
        }
    }
}
