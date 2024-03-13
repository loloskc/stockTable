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
                if (!await roleManager.RoleExistsAsync(UserRole.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRole.User));

                //Users
                var userManager = service.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "template@gmail.com";

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
                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,

                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRole.User);
                }
            }
        }
    }
}
