using FrontToBackFlowers.Areas.AdminPanel.Models;
using FrontToBackFlowers.DAL;
using FrontToBackFlowers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBackFlowers.Areas.AdminPanel.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly FlowerDbContext _flowerDbContext;

        public CategoryController(FlowerDbContext flowerDbContext)
        {
            _flowerDbContext = flowerDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _flowerDbContext.Categories.ToListAsync();
            return View(categories);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id is null) return NotFound();
            var category = await _flowerDbContext.Categories.FindAsync(id);
            if(category is null) return NotFound();
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateModel category)
        {
            if (!ModelState.IsValid)  return View();
            var existCategoryName = await _flowerDbContext.Categories.AnyAsync(x => x.Name.ToLower().Equals(category.Name.ToLower()));
            if (existCategoryName)
            {
                ModelState.AddModelError("name", "The same name cannot be repeated");
                return View();
            }

            var categoryEntity = new Category
            {
                Name = category.Name,
                Description = category.Description
            };
            await _flowerDbContext.Categories.AddAsync(categoryEntity);
            await _flowerDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id is null) return NotFound();

            var category = await _flowerDbContext.Categories.FindAsync(id);

            if (category is null) return NotFound();

            return View(new CategoryUpdateModel
            {
                Name = category.Name,
                Description = category.Description
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, CategoryUpdateModel model)
        {
            if (id is null) return NotFound();
        
            if(!ModelState.IsValid) return View();

            var category = await _flowerDbContext.Categories.FindAsync(id);

            if (category.Id != id) return BadRequest();

            if (category is null) return NotFound();

            var isExistCategoryName = await _flowerDbContext.Categories.AnyAsync(c => c.Name.ToLower() == model.Name.ToLower() && c.Id != id);

            if(isExistCategoryName)
            {
                ModelState.AddModelError("Name", "The same name cannot be repeated");
                return View();
            }

            category.Name = model.Name;
            category.Description = model.Description;

            await _flowerDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id is null) return NotFound();
            var category = await _flowerDbContext.Categories.FindAsync(id);

            if(category is null) return NotFound();

            _flowerDbContext.Categories.Remove(category);

            await _flowerDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
