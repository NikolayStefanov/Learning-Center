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

            var categoriesDictionary = new Dictionary<string, string>
            {
                { "Personal Development", "personalDevelopmentImage.jpg" },
                { "Business", "businessImage.png" },
                { "IT & Software", "ITandSoftwareImage.jpg" },
                { "Design", "designImage.jpg" },
                { "Marketing", "marketingImage.jpg" },
                { "Lifestyle", "lifestyleImage.jpg" },
                { "Photography & Video", "photographyImage.jpg" },
                { "Health & Fitness", "fitnessImage.jpg" },
            };

            foreach (var category in categoriesDictionary)
            {
                await dbContext.Categories.AddAsync(new Category { Title = category.Key, ImageName = category.Value });
            }
        }
    }
}
