namespace LearningCenter.Web.ViewModels.Account
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using LearningCenter.Web.ViewModels.About;

    public class ProfileViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public string Email { get; set; }

        public ProfilePictureViewModel ProfilePicture { get; set; }

        public DateTime BirthDate { get; set; }

        public int Age => DateTime.UtcNow.Year - this.BirthDate.Year;

        public int LecturesCount { get; set; }

        public int CoursesCount { get; set; }

        public IEnumerable<UserCoursesViewModel> Courses { get; set; }

        public IEnumerable<FeedbackViewModel> Feedbacks { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, ProfileViewModel>()
                .ForMember(x => x.Feedbacks, opt => opt.MapFrom(u => u.Lecturer.Feedbacks))
                .ForMember(x => x.CoursesCount, opt => opt.MapFrom(u => u.Courses.Count));
        }
    }
}
