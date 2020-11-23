namespace LearningCenter.Web.ViewModels.About
{
    using System;

    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class TopLectorerViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public ProfilePictureViewModel ProfilePicture { get; set; }

        public DateTime BirthDate { get; set; }

        public int Age => DateTime.UtcNow.Year - this.BirthDate.Year;

        public int LecturesCount { get; set; }
    }
}
