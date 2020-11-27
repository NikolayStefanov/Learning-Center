namespace LearningCenter.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LearningCenter.Data.Models;

    public class SubCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.SubCategories.Any())
            {
                return;
            }

            var subCategoryDictionary = new Dictionary<string, List<string>>
            {
                { "Personal Development", new List<string> { "Leadership", "Career Development", "Parenting & Relationships", "Happiness", "Confidence", "Stress Management", "Memory and Study Skills", "Motivation", "Other Personal Development", } },
                { "IT & Software", new List<string> { "IT Certifiaction", "Network & Security", "Hardware", "Operating Systems", "Software Development", "Other IT & Software" } },
                { "Design", new List<string> { "Web Design", "Graphic Design & Illustration", "Design Tools", "UX Design", "Game Design", "3D & Animation", "Fashion Design", "Architectural Design", "Interior Design", "Other Design", } },
                { "Marketing", new List<string> { "Digital Marketing", "SEO", "Social Media Marketing", "Branding", "Marketing Fundamentals", "Marketing Analytics & Automation", "Other Marketing", } },
                { "Lifestyle", new List<string> { "Arts & Crafts", "Beauty & Makeup", "Esoteric Practices", "Food & Beverage", "Gaming", "Home Improvement", "Pet Care & Training", "Travel", "Other Lifestyle" } },
                { "Photography & Video", new List<string> { "Digital Photography", "Photography", "Portrait Photography", "Photography Tools & Edit", "Video Design", "Video Tools & Edit", "Other Photography & Video" } },
                { "Business", new List<string> { "Communications", "Management", "Sales", "Business Strategy", "Operations", "Project Management", "Business Law", "Other Business" } },
                { "Health & Fitness", new List<string> { "Fitness", "General Health", "Sports", "Yoga", "Mental Health", "Dieting", "Self Defence", "Safety & First Aid", "Dance", "Meditation", "Other Health & Fitness" } },
            };

            foreach (var subCategory in subCategoryDictionary)
            {
                var currCategory = dbContext.Categories.Where(x => x.Title == subCategory.Key).FirstOrDefault();

                foreach (var subCategoryTitle in subCategory.Value)
                {
                    var newSubcateogory = new SubCategory { Title = subCategoryTitle, Category = currCategory };
                    currCategory.SubCategories.Add(newSubcateogory);
                    await dbContext.SubCategories.AddAsync(newSubcateogory);
                }
            }
        }
    }
}
