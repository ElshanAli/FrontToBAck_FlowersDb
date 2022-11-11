using FrontToBackFlowers.DAL;
using Microsoft.EntityFrameworkCore;

namespace FrontToBackFlowers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
          

            builder.Services.AddMvc();
            builder.Services.AddDbContext<FlowerDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            var app = builder.Build();
            app.UseRouting();
            app.UseStaticFiles();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                app.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");
            });
           

            app.Run();
        }
    }
}