using FrontToBackFlowers.Models;
using Microsoft.EntityFrameworkCore;

namespace FrontToBackFlowers.DAL
{
    public class FlowerDbContext : DbContext
    {
        public FlowerDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }
        public DbSet<FlowerExpert> FlowerExperts { get; set; }
    }
}
