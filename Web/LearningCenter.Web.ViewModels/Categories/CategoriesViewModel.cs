namespace LearningCenter.Web.ViewModels.Categories
{
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class CategoriesViewModel : IMapFrom<Category>
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }

    }
}
