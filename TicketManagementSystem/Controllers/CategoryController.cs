using Microsoft.AspNetCore.Mvc;
using TicketManagementSystem.DTOs.Category;
using TicketManagementSystem.Interfaces;
using TicketManagementSystem.Models;

namespace TicketManagementSystem.Controllers
{
    public class CategoryController(IcategoryService service) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var categories = await service.GetAll();
            return View(categories);
        }

       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            await service.Create(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await service.GetById(id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UpdateCategoryDto dto)
        {
            await service.Update(id, dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
