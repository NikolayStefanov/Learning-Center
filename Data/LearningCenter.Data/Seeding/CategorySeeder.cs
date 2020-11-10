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
                { "Personal Development", "https://pbs.twimg.com/profile_images/879620476435386368/yrF-iHUz.jpg" },
                { "Business", "https://lh3.googleusercontent.com/proxy/2JZvUqyjx0i_ddDSBza9kg3EH_q7jIPoMC6gOiLK1y8tdLAFmKDaBcEE8qN7axElX_8WW0eXY2bFKDaVQnbb2riWLNTHUvsQBYvL42njebxB1fizMrxIeqcOtWU9Ew" },
                { "IT & Software", "https://www.riscure.com/uploads/2020/03/software-development-resized-600x600-c-1.jpg" },
                { "Desing", "https://iotgossiper.files.wordpress.com/2019/08/design-thinking-edited.jpg" },
                { "Marketing", "https://bestseocompanyworld.com/wp-content/uploads/2020/04/advanced-marketing-strategy.jpg" },
                { "Lifestyle", "https://nwzimg.wezhan.hk/contents/sitefiles3602/18011019/images/1178205.jpg" },
                { "Photography & Video", "https://s3.amazonaws.com/pbblogassets/uploads/2017/08/27130641/Photography-Cover.jpg" },
                { "Fitness", "https://www.onlinelogomaker.com/blog/wp-content/uploads/2017/07/fitness-logo.jpg" },
                { "Music", "https://images.macrumors.com/t/MKlRm9rIBpfcGnjTpf6ZxgpFTUg=/1600x1200/smart/article-new/2018/05/apple-music-note.jpg" },
                { "High School Teaching", "https://wittywiral.files.wordpress.com/2018/08/education.jpg" },
            };

            foreach (var category in categoriesDictionary)
            {
                await dbContext.Categories.AddAsync(new Category { Title = category.Key, ImageUrl = category.Value });
            }
        }
    }
}
