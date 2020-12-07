namespace LearningCenter.Web.ViewModels.Courses
{
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class ReviewViewModel : IMapFrom<Review>
    {
        public string Content { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }
    }
}
