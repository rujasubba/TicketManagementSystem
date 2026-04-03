using Microsoft.AspNetCore.Mvc;
using TicketManagementSystem.DTOs.Department;
using TicketManagementSystem.Interfaces;

namespace TicketManagementSystem.Controllers
{
    public class DepartmentController(IDepartmentService service) : Controller
    {
        public async Task <IActionResult> Index()
        {
            var departmentList = await service.GetAllAsync();

            return View(departmentList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task <IActionResult> Create(CreateDepartmentDto model)
        {
            if (model == null)
            {
                throw new Exception("Nothing entered");
            }
            var department = await service.CreateAsync(model);
            if (department == null)
            {
                ModelState.AddModelError("", "Failed to create department");
                return PartialView(model);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await service.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return PartialView(department);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await service.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}

