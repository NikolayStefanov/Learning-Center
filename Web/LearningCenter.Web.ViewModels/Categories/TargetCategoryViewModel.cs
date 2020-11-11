namespace LearningCenter.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using LearningCenter.Web.ViewModels.Courses;
    using LearningCenter.Web.ViewModels.SubCategories;

    public class TargetCategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<SubCategoryViewModel> SubCategories { get; set; }

        public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}
