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
                { "Personal Development", "https://lh3.googleusercontent.com/proxy/6_UkcxOMHnu5RDFvM2n62sdBuf9OvdCCfBrhJ5hc7THre-ATW8n48m72A9vuQq3tIiymfnQQaJk2OReeP0cngmdQ-tUOHchaW8Qaj2NuWkrLE12ka_GuKOO-IktU2br8_6gopFvPZ5ht" },
                { "Business", "https://eduadvisor.my/articles/wp-content/uploads/2018/04/Types-of-business-degrees-Feature.png" },
                { "IT & Software", "https://www.riscure.com/uploads/2020/03/software-development-resized-600x600-c-1.jpg" },
                { "Design", "https://iotgossiper.files.wordpress.com/2019/08/design-thinking-edited.jpg" },
                { "Marketing", "https://bestseocompanyworld.com/wp-content/uploads/2020/04/advanced-marketing-strategy.jpg" },
                { "Lifestyle", "https://nwzimg.wezhan.hk/contents/sitefiles3602/18011019/images/1178205.jpg" },
                { "Photography & Video", "https://s3.amazonaws.com/pbblogassets/uploads/2017/08/27130641/Photography-Cover.jpg" },
                { "Health & Fitness", "https://www.onlinelogomaker.com/blog/wp-content/uploads/2017/07/fitness-logo.jpg" },
            };

            foreach (var category in categoriesDictionary)
            {
                await dbContext.Categories.AddAsync(new Category { Title = category.Key, ImageUrl = category.Value });
            }
        }
    }
}
