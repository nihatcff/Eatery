using Eatery.Enums;
using Eatery.Models;
using Eatery.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;

namespace Eatery.Helpers
{
    public class DbContextInitializer
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly AdminVM _admin;
        public DbContextInitializer(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
            _admin = _configuration.GetSection("AdminSettings").Get<AdminVM>() ?? new();
        }
        public async Task InitializeDatabaseAsync()
        {
            await CreateRoles();
            await CreateAdmin();

        }

        private async Task CreateAdmin()
        {
            AppUser user = new()
            {
                Fullname = _admin.Fullname,
                Email = _admin.Email,
                UserName = _admin.UserName
            };

            var result = await _userManager.CreateAsync(user, _admin.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, IdentityRoles.Admin.ToString());
            }
        }

        private async Task CreateRoles()
        {
            foreach (var role in Enum.GetNames(typeof(IdentityRoles)))
            {
                await _roleManager.CreateAsync(new()
                {
                    Name = role
                });
            }
        }
    }
}
