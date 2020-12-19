namespace LearningCenter.Web.ViewModels.Account
{
    using System.Collections.Generic;

    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class LecturerViewModel : IMapFrom<Lecturer>
    {
        public string Id { get; set; }

        public IEnumerable<PersonalCourseViewModel> Courses { get; set; }

        public IEnumerable<FeedbackViewModel> Feedbacks { get; set; }
    }
}
