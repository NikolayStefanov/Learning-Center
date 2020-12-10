namespace LearningCenter.Web.ViewModels.Courses
{
    using System;
    using System.Collections.Generic;

    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class LectureViewModel : IMapFrom<Lecture>
    {
        public string VideoUrl { get; set; }

        public string ThumbnailUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ShortDescription => this.Description.Substring(0, 65) + "...";

        public IEnumerable<AdditionalResourceViewModel> AdditionalResources{ get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
