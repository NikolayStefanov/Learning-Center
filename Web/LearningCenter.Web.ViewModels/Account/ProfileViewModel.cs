namespace LearningCenter.Web.ViewModels.Account
{
    using System;
    using System.Collections.Generic;

    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class ProfileViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public string Email { get; set; }

        public string ProfilePictureUrl { get; set; }

        public DateTime BirthDate { get; set; }

        public int Age => DateTime.UtcNow.Year - this.BirthDate.Year;

        public int LecturesCount { get; set; }

        public IEnumerable<UserCoursesViewModel> Courses { get; set; }

        public LecturerViewModel Lecturer { get; set; }
    }
}
