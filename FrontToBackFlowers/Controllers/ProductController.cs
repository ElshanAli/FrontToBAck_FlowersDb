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
        private int _productsCount;

        public ProductController(FlowerDbContext flowerDbContext)
        {
            _flowerDbContext = flowerDbContext;
            _productsCount = _flowerDbContext.Products.Count();
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.productCount = _productsCount;
            List<Product> products = await _flowerDbContext.Products.Include(x => x.Category).Take(4).ToListAsync();
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

    

        public async Task<IActionResult> PartialProduct(int toPass)
        {

            if (toPass >= _productsCount) return BadRequest();


            var products = await _flowerDbContext.Products.Include(x => x.Category).Skip(toPass).Take(4).ToListAsync();

            return PartialView("_ProductListPartialView", products);
        }
    }
}
