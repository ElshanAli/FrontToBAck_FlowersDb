using FrontToBackFlowers.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrontToBackFlowers.ViewComponents
{
    public class BioViewComponent : ViewComponent
    {
        private readonly FlowerDbContext _flowerDbContext;

        public BioViewComponent(FlowerDbContext flowerDbContext)
        {
            _flowerDbContext = flowerDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var bio = await _flowerDbContext.SocialMedias.ToListAsync();

            return View(bio);
        }
    }
}
