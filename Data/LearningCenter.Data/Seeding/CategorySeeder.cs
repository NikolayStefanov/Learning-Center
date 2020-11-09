namespace LearningCenter.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LearningCenter.Data.Models;
    using Microsoft.EntityFrameworkCore.Internal;

    public class CategorySeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new List<string> { "Development", "Business", "IT & Software", "Personal Development", "Desing", "Marketing", "Lifestyle", "Photography & Video", "Health & Fitness", "Music", "High School Teaching" };

            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(new Category { Title = category });
            }
        }
    }
}
