using FrontToBackFlowers.DAL;
using FrontToBackFlowers.Data;
using FrontToBackFlowers.Models;
using FrontToBackFlowers.Models.IdentityModels;
using FrontToBackFlowers.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace FrontToBackFlowers
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
          

            builder.Services.AddMvc();
            builder.Services.AddDbContext<FlowerDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                builder =>
                {
                    builder.MigrationsAssembly("FrontToBackFlowers");

                }));

            builder.Services.AddIdentity<IdentityOfUser, IdentityRole>(option =>
            {
                option.Lockout.MaxFailedAccessAttempts = 3;
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                option.SignIn.RequireConfirmedEmail = true;

                option.User.RequireUniqueEmail = true;

                option.Password.RequireUppercase = false;

                option.Password.RequireLowercase = false;

                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<FlowerDbContext>()
                .AddDefaultTokenProviders().AddErrorDescriber<LocalizedIdentityErrorDescriber>();
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

            builder.Services.AddTransient<IMailService, MailManager>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FlowerDbContext>();
                var userManger = scope.ServiceProvider.GetRequiredService<UserManager<IdentityOfUser>>();
                var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                

                var initOfData = new DataInitializer(dbContext, userManger, roleManger);
                await initOfData.SeedData();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/ErrorPage");
            }
          
            app.UseStatusCodePagesWithReExecute("/ErrorPage/ErrorAction", "?code={0}");

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
          
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                app.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");
            });
           

            await app.RunAsync();
        }
    }
}