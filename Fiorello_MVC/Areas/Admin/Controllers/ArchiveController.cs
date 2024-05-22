using Fiorello_MVC.Data;
using Fiorello_MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArchiveController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _appDbContext;
        public ArchiveController(ICategoryService categoryService, AppDbContext appDbContext)
        {
            _categoryService = categoryService;
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> CategoryArchive()
        {
            return View(await _categoryService.GetAllArchiveAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SetToArchieve(int? id)
        {
            if (id == null) return BadRequest();

            var category = await _categoryService.GetById((int)id);

            if (category == null) return NotFound();

            category.SoftDeleted = false;

            await _appDbContext.SaveChangesAsync();
            return Ok(category);
        }
    }
}
