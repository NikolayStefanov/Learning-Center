namespace LearningCenter.Web.ViewModels.Courses
{
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class CourseViewModel : IMapFrom<Course>
    {
        public string Title { get; set; }

        public decimal Price { get; set; }
    }
}
