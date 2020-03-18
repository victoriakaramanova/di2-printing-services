using Di2.Common;
using Di2.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Di2.Data.Seeding
{
    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.ApplicationUsers.Any())
            {
                return;
            }

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // HOW TO ADD THE PASS TO USER WHEN STUCK TO THIS INTERFACE?!?!!
            var user = new ApplicationUser
            {
                UserName = "user",
                Email = "user@user.com",
                EmailConfirmed = true,
            };

            var admin = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@admin.com",
                EmailConfirmed = true,
            };

            var adminPass = "admin";
            var userPass = "user";

            await userManager.CreateAsync(admin, adminPass);
            await userManager.CreateAsync(user, userPass);
            await dbContext.AddAsync(admin);
            await dbContext.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddToRoleSeedAsync(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationUser> roleManager)
        {
            var user = dbContext.ApplicationUsers.FirstOrDefault(x => x.UserName == "user");
            var admin = dbContext.ApplicationUsers.FirstOrDefault(x => x.UserName == "admin");
            await userManager.AddToRoleAsync(user, GlobalConstants.UserRoleName);
            await userManager.AddToRoleAsync(admin, GlobalConstants.AdministratorRoleName);
        }
}
}
