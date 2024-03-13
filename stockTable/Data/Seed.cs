using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using stockTable.Models;
using System.Net;

namespace stockTable.Data
{
    public class Seed
    {
        public static async Task SeedUserAndRolesAsync(IApplicationBuilder app)
        {
            using (var service = app.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = service.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRole.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
                if (!await roleManager.RoleExistsAsync(UserRole.Reader))
                    await roleManager.CreateAsync(new IdentityRole(UserRole.Reader));
                if(!await roleManager.RoleExistsAsync(UserRole.Editor))
                    await roleManager.CreateAsync(new IdentityRole(UserRole.Editor));

                //Users
                var userManager = service.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "admin";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = "Admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAdminUser, "admin");
                    await userManager.AddToRoleAsync(newAdminUser, UserRole.Admin);
                }
            }
        }
    }
}
