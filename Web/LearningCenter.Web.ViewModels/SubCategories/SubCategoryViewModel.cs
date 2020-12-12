namespace LearningCenter.Web.ViewModels.SubCategories
{
    using System.Collections.Generic;

    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using LearningCenter.Web.ViewModels.Categories;

    public class SubCategoryViewModel : PaginationViewModel, IMapFrom<SubCategory>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}
