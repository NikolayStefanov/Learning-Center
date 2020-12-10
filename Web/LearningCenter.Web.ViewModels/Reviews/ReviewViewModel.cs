namespace LearningCenter.Web.ViewModels.Reviews
{
    using System;

    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class ReviewViewModel : IMapFrom<Review>
    {
        public string AuthorId { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public string AuthorProfilePictureUrl { get; set; }

        public string AuthorFullName => $"{this.AuthorFirstName} {this.AuthorLastName}";

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
