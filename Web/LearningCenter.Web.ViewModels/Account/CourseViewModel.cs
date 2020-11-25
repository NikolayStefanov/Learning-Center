namespace LearningCenter.Web.ViewModels.Account
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

        public int ReviewsCount { get; set; }

        public double AverageRating { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Course, CourseViewModel>()
                .ForMember(x => x.AverageRating, opt => opt.MapFrom(c => c.Ratings.Average(r => r.Value)));
        }
    }
}
