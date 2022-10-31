using FrontToBackFlowers.DAL;
using FrontToBackFlowers.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBackFlowers.Controllers
{
    public class HomeController : Controller
    {
        private readonly FlowerDbContext _flowerDbContext;

        public HomeController(FlowerDbContext flowerDbContext)
        {
            _flowerDbContext = flowerDbContext;
        }

        public IActionResult Index()
        {
            var sliderImages = _flowerDbContext.SliderImages.ToList();
            var slider = _flowerDbContext.Sliders.SingleOrDefault();
            var categories = _flowerDbContext.Categories.ToList();
            var products = _flowerDbContext.Products.ToList();
            var flowerExperts = _flowerDbContext.FlowerExperts.ToList();

            var homeViewModel = new HomeViewModel
            {
                SliderImages = sliderImages,
                Slider = slider,
                Categories = categories,
                Products = products,
                FlowerExperts=flowerExperts
            };
            return View(homeViewModel);
        }
    }
}
