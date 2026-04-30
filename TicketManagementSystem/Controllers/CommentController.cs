using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketManagementSystem.DTOs.Comment;
using TicketManagementSystem.Interfaces;
using TicketManagementSystem.Models;
using TicketManagementSystem.Services;

namespace TicketManagementSystem.Controllers
{
    public class CommentController(ICommentService service) : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCommentDto dto)
        {
           
            var comment = await service.CreateAsync(dto, UserId);
           
            return Ok(
                new
                {
                    content = comment.Content,
                   createdBy = UserId,
                    createdDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm tt"),
                    attachments = comment.SavedAttachments?.Select(a => new
                    {
                        fileName = a.FileName,
                        filePath = a.FilePath
                    })

                });
        }
    }
}
