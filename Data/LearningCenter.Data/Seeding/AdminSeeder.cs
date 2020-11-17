namespace LearningCenter.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LearningCenter.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using static LearningCenter.Common.GlobalConstants;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var isAdminExists = await userManager.FindByNameAsync(AdminFirstName);

            if (isAdminExists != null)
            {
                return;
            }

            var admin = new ApplicationUser
            {
                UserName = AdminEmail,
                FirstName = AdminFirstName,
                LastName = AdminLastName,
                Email = AdminEmail,
                BirthDate = new DateTime(1997, 3, 17),
                Gender = Models.Enums.GenderEnum.Male,
            };

            var result = await userManager.CreateAsync(admin, AdminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, AdministratorRoleName);
            }
        }
    }
}
