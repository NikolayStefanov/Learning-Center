namespace LearningCenter.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Data.Models.Enums;
    using LearningCenter.Services.Mapping;
    using LearningCenter.Web.ViewModels;
    using LearningCenter.Web.ViewModels.About;
    using Moq;
    using Xunit;

    public class AboutServiceTests
    {
        public AboutServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly, Assembly.Load("LearningCenter.Services.Data.Tests"));
        }

        [Fact]
        public void GetTopLecturersMustReturnTopFourLecturerWithMostCoursesMade()
        {
            var lecturersDb = new List<ApplicationUser>();
            var categoriesDb = new List<Category>();

            var mockCategoriesRepo = new Mock<IDeletableEntityRepository<Category>>();
            var mockLecturersRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockLecturersRepo.Setup(x => x.All()).Returns(lecturersDb.AsQueryable());

            var courses = new List<Course>
            {
                new Course(),
                new Course(),
                new Course(),
                new Course(),
                new Course(),
                new Course(),
            };

            var lectures = new List<Lecture>
            {
                new Lecture(),
                new Lecture(),
                new Lecture(),
                new Lecture(),
                new Lecture(),
                new Lecture(),
            };

            var lecturers = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", FirstName = "Nikolay1", LastName = "Stefanov1", ProfilePicture = new ProfilePicture { Url = "picUrl" }, BirthDate = new DateTime(1997, 3, 17), Lectures = lectures.Take(1).ToList(), UserType = UserTypeEnum.Lecturer, Lecturer = new Lecturer { Courses = courses.Take(6).ToList() } },
                new ApplicationUser { Id = "2", FirstName = "Nikolay2", LastName = "Stefanov2", ProfilePicture = new ProfilePicture { Url = "picUrl" }, BirthDate = new DateTime(1997, 3, 17), Lectures = lectures.Take(2).ToList(), UserType = UserTypeEnum.Lecturer, Lecturer = new Lecturer { Courses = courses.Take(6).ToList() } },
                new ApplicationUser { Id = "3", FirstName = "Nikolay3", LastName = "Stefanov3", ProfilePicture = new ProfilePicture { Url = "picUrl" }, BirthDate = new DateTime(1997, 3, 17), Lectures = lectures.Take(3).ToList(), UserType = UserTypeEnum.Lecturer, Lecturer = new Lecturer { Courses = courses.Take(6).ToList() } },
                new ApplicationUser { Id = "4", FirstName = "Nikolay4", LastName = "Stefanov4", ProfilePicture = new ProfilePicture { Url = "picUrl" }, BirthDate = new DateTime(1997, 3, 17), Lectures = lectures.Take(4).ToList(), UserType = UserTypeEnum.Lecturer, Lecturer = new Lecturer { Courses = courses.Take(6).ToList() } },
                new ApplicationUser { Id = "5", FirstName = "Nikolay5", LastName = "Stefanov5", ProfilePicture = new ProfilePicture { Url = "picUrl" }, BirthDate = new DateTime(1997, 3, 17), Lectures = lectures.Take(5).ToList(), UserType = UserTypeEnum.Lecturer, Lecturer = new Lecturer { Courses = courses.Take(6).ToList() } },
            };

            lecturersDb.AddRange(lecturers);
            var service = new AboutService(mockLecturersRepo.Object, mockCategoriesRepo.Object);

            var resultObj = service.GetTopLecturers<TopLectorerViewModel>();

            Assert.Equal("Nikolay5", resultObj.First().FirstName);
            Assert.Equal(4, resultObj.Count());

            lecturersDb[0].Lectures.Add(new Lecture());
            lecturersDb[0].Lectures.Add(new Lecture());
            lecturersDb[0].Lectures.Add(new Lecture());
            lecturersDb[0].Lectures.Add(new Lecture());
            lecturersDb[0].Lectures.Add(new Lecture());
            lecturersDb[0].Lectures.Add(new Lecture());

            var newResultObj = service.GetTopLecturers<TopLectorerViewModel>();
            Assert.Equal("Nikolay1", newResultObj.First().FirstName);
            Assert.Equal("Nikolay5", newResultObj.Skip(1).First().FirstName);
            Assert.Equal("Nikolay4", newResultObj.Skip(2).First().FirstName);
            Assert.Equal(4, newResultObj.Count());
        }
    }
}
