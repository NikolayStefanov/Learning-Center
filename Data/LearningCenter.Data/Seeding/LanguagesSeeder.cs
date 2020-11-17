namespace LearningCenter.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LearningCenter.Data.Models;

    public class LanguagesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Languages.Any())
            {
                return;
            }

            var languagesList = new List<string>
            {
                "Bulgarian",
                "English",
                "Russian",
                "Spanish",
                "French",
                "Italian",
            };

            foreach (var language in languagesList)
            {
               await dbContext.Languages.AddAsync(new Language { Name = language });
            }
        }
    }
}
