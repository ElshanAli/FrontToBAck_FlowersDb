using FrontToBackFlowers.DAL;
using FrontToBackFlowers.Models;
using FrontToBackFlowers.Services;
using FrontToBackFlowers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FrontToBackFlowers.Controllers
{
    public class HomeController : Controller
    {
        private readonly FlowerDbContext _flowerDbContext;
        private readonly IMailService _mailService;
        public HomeController(FlowerDbContext flowerDbContext, IMailService mailService)
        {
            _flowerDbContext = flowerDbContext;
            _mailService = mailService;
        }
        public async Task<IActionResult> Index()
        {
            

            var sliderImages = _flowerDbContext.SliderImages.ToList();
            var slider = _flowerDbContext.Sliders.SingleOrDefault();
            var categories = _flowerDbContext.Categories.ToList();
            var products = _flowerDbContext.Products.Include(c => c.Category).ToList();
            var flowerExperts = _flowerDbContext.FlowerExperts.ToList();

            var homeViewModel = new HomeViewModel
            {
                SliderImages = sliderImages,
                Slider = slider,
                Categories = categories,
                Products = products,
                FlowerExperts = flowerExperts
            };
            return View(homeViewModel);

        }

        public IActionResult Search(string searchProduct)
        {
            var products = _flowerDbContext.Products.Where(p => p.Name.ToLower().Contains(searchProduct.ToLower())).ToList();

            return PartialView("_SearchProductPartialView", products);
        }
   
        
    }

}
