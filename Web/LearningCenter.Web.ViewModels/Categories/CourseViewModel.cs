namespace LearningCenter.Web.ViewModels.Categories
{
    using System.Linq;

    using AutoMapper;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class CourseViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ThumbnailUrl { get; set; }

        public string SubCategoryTitle { get; set; }

        public string CategoryTitle { get; set; }

        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public double AverageRating { get; set; }

        public int RatingsCount { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public string AuthorName => $"{this.AuthorFirstName} {this.AuthorLastName}";

        public string AuthorId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Course, CourseViewModel>()
                .ForMember(x => x.AverageRating, opt => opt.MapFrom(x => x.Ratings.Average(r => r.Value)));
        }
    }
}