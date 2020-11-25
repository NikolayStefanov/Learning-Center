namespace LearningCenter.Web.ViewModels.Account
{

    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class FeedbackViewModel : IMapFrom<Feedback>
    {
        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public string AuthorFullName => $"{this.AuthorFirstName} {this.AuthorLastName}";

        public string Content { get; set; }
    }
}
