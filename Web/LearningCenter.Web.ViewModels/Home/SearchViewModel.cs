namespace LearningCenter.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using LearningCenter.Web.ViewModels.Categories;

    public class SearchViewModel : PaginationViewModel
    {
        public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}
