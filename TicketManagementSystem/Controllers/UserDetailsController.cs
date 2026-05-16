using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using TicketManagementSystem.DTOs;
using TicketManagementSystem.Interfaces;

namespace TicketManagementSystem.Controllers
{
    public class UserDetailsController(IUserDetailsService service) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await service.GetAllUsersAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var formData = await service.GetFormDataAsync();

            ViewBag.UserSelectList = new SelectList(formData.Users, "Id", "DisplayName");

            ViewBag.UsersJson = JsonSerializer.Serialize(formData.Users, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            ViewBag.Departments = new SelectList(formData.Departments, "Id", "Name");

            return View(new UserDetailsDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDetailsDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await service.CreateAsync(dto);
            return Ok(result);
        }
    }
}
