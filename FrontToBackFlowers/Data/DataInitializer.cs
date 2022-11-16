using FrontToBackFlowers.DAL;
using FrontToBackFlowers.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FrontToBackFlowers.Data
{
    public class DataInitializer
    {
        private readonly FlowerDbContext _context;
        private readonly UserManager<IdentityOfUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataInitializer(FlowerDbContext context, UserManager<IdentityOfUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedData()
        {   
            await _context.Database.MigrateAsync();

            var roles = new List<string> { RoleConstants.AdminRole, RoleConstants.UserRole };

            foreach (var role in roles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                    continue;

                var result = await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = role
                });

                if (!result.Succeeded)
                {
                    //logging
                }
            }

            var user = new IdentityOfUser
            {
                FullName = "Admin admin",
                UserName = "admin",
                Email = "admin@code.edu.az"
            };

            if (await _userManager.FindByNameAsync(user.UserName) != null)
                return;

            await _userManager.CreateAsync(user, "1234567");
            await _userManager.AddToRoleAsync(user, RoleConstants.AdminRole);
        }
    }
}
