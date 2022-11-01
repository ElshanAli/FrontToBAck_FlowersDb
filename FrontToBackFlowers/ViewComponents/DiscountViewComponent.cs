using FrontToBackFlowers.DAL;
using FrontToBackFlowers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBackFlowers.ViewComponents
{
    public class DiscountViewComponent : ViewComponent
    {
        private readonly FlowerDbContext _flowerDbContext;

        public DiscountViewComponent(FlowerDbContext flowerDbContext)
        {
            _flowerDbContext = flowerDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Product> products = await _flowerDbContext.Products.Where(x =>
            x.Discount != null).Include(x => 
            x.Category).ToListAsync();
            return View(products);
        }
    }
}
