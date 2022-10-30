using FrontToBackFlowers.DAL;
using FrontToBackFlowers.Models;
using FrontToBackFlowers.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBackFlowers.Controllers
{
    public class ProductController : Controller
    {
        private readonly FlowerDbContext _flowerDbContext;

        public ProductController(FlowerDbContext flowerDbContext)
        {
            _flowerDbContext = flowerDbContext;
        }

        public IActionResult Index()
        {
            var products = _flowerDbContext.Products.Include(x => x.Category).ToList();
            var homeViewModel = new HomeViewModel
            {
                Products = products
            };
            return View(homeViewModel);
        }

        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest();

            var product = _flowerDbContext.Products.Include(x => x.Category).SingleOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            return View(product);
        }
    }
}
