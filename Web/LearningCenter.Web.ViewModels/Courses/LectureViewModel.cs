namespace LearningCenter.Web.ViewModels.Courses
{
    using System;

    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class LectureViewModel : IMapFrom<Lecture>
    {
        public string VideoId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
