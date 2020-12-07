namespace LearningCenter.Web.ViewModels.Categories
{

    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class SubCategoryViewModel : IMapFrom<SubCategory>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CoursesCount { get; set; }
    }
}
