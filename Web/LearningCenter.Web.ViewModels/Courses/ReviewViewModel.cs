﻿namespace LearningCenter.Web.ViewModels.Courses
{
    using System;

    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class ReviewViewModel : IMapFrom<Review>
    {
        public string Content { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public string AuthorProfilePictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
