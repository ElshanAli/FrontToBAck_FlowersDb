using FrontToBackFlowers.Areas.AdminPanel.Models;
using FrontToBackFlowers.DAL;
using FrontToBackFlowers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBackFlowers.Areas.AdminPanel.Controllers
{
    public class SlideImageController : BaseController
    {
        private readonly FlowerDbContext _flowerDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SlideImageController(FlowerDbContext flowerDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _flowerDbContext = flowerDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var slideImages = await _flowerDbContext.SliderImages.ToListAsync();
            return View(slideImages);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SlideImageCreateModel model)
        {
            if (!ModelState.IsValid) return View();
            if (!model.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image", "Must be selected image");
                return View();
            }
            if (model.Image.Length > 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Image size can be maximum 1mb");
                return View();
            }
            var unicalName = $"{Guid.NewGuid}-{model.Image.FileName}";
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "img", unicalName);
            var fStream = new FileStream(imagePath, FileMode.Create);
            await model.Image.CopyToAsync(fStream);
            await _flowerDbContext.SliderImages.AddAsync(new SliderImage { 
            
            Name = unicalName,
            });
            await _flowerDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
