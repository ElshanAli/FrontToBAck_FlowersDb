using FrontToBackFlowers.Models;
using FrontToBackFlowers.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FrontToBackFlowers.DAL
{
    public class FlowerDbContext : IdentityDbContext<IdentityOfUser>
    {
        public FlowerDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }
        public DbSet<FlowerExpert> FlowerExperts { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
    }
}
