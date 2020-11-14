namespace LearningCenter.Web.ViewModels.Categories
{
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class CategoriesViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageName { get; set; }
    }
}
