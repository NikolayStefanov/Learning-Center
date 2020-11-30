namespace LearningCenter.Web.ViewModels.Account
{
    using System.Linq;

    using AutoMapper;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class PersonalCourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ThumbnailUrl { get; set; }

        public string SubCategoryTitle { get; set; }

        public string CategoryTitle { get; set; }

        public int CategoryId { get; set; }
    }
}
