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
        private List<ApplicationUser> lecturersDb;
        private List<Category> categoriesDb;
        private AboutService service;

        public AboutServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly, Assembly.Load("LearningCenter.Services.Data.Tests"));
            this.lecturersDb = new List<ApplicationUser>();
            this.categoriesDb = new List<Category>();

            var mockCategoriesRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockCategoriesRepo.Setup(x => x.AllAsNoTracking()).Returns(this.categoriesDb.AsQueryable());
            var mockLecturersRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockLecturersRepo.Setup(x => x.AllAsNoTracking()).Returns(this.lecturersDb.AsQueryable());
            mockLecturersRepo.Setup(x => x.All()).Returns(this.lecturersDb.AsQueryable());

            this.service = new AboutService(mockLecturersRepo.Object, mockCategoriesRepo.Object);
        }

        [Fact]
        public void GetTopLecturersMustReturnTopFourLecturerWithMostCoursesMade()
        {
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

            this.lecturersDb.AddRange(lecturers);

            var resultObj = this.service.GetTopLecturers<TopLectorerViewModel>();

            Assert.Equal("Nikolay5", resultObj.First().FirstName);
            Assert.Equal(4, resultObj.Count());

            this.lecturersDb[0].Lectures.Add(new Lecture());
            this.lecturersDb[0].Lectures.Add(new Lecture());
            this.lecturersDb[0].Lectures.Add(new Lecture());
            this.lecturersDb[0].Lectures.Add(new Lecture());
            this.lecturersDb[0].Lectures.Add(new Lecture());
            this.lecturersDb[0].Lectures.Add(new Lecture());

            var newResultObj = this.service.GetTopLecturers<TopLectorerViewModel>();
            Assert.Equal("Nikolay1", newResultObj.First().FirstName);
            Assert.Equal("Nikolay5", newResultObj.Skip(1).First().FirstName);
            Assert.Equal("Nikolay4", newResultObj.Skip(2).First().FirstName);
            Assert.Equal(4, newResultObj.Count());
        }

        [Fact]
        public void GetAboutInfoMustReturnCorrectlyFilledViewModel()
        {
            var courses = new List<Course>
            {
                new Course(),
                new Course(),
                new Course(),
                new Course(),
                new Course(),
            };
            var subcategories = new List<SubCategory>
                {
                    new SubCategory { Courses = courses },
                    new SubCategory { Courses = courses },
                    new SubCategory { Courses = courses },
                    new SubCategory { Courses = courses },
                };

            var lectures = new List<Lecture>
            {
                new Lecture(),
                new Lecture(),
            };

            this.categoriesDb.AddRange(new List<Category>
            {
                new Category { SubCategories = subcategories },
                new Category { SubCategories = subcategories },
                new Category {SubCategories = subcategories },
            });


            this.lecturersDb.AddRange(new List<ApplicationUser>
            {
                new ApplicationUser { Lectures = lectures },
                new ApplicationUser { Lectures = lectures },
                new ApplicationUser { Lectures = lectures },
            });

            var resultObj = this.service.GetAboutInfo();
            Assert.Equal(3, resultObj.CategoriesCount);
            Assert.Equal(12, resultObj.SubCategoriesCount);
            Assert.Equal(60, resultObj.CoursesCount);
            Assert.Equal(3, resultObj.LecturersCount);
            Assert.Equal(6, resultObj.LecturesCount);

            this.categoriesDb.Clear();
            this.lecturersDb.Clear();

            var invalidResult = this.service.GetAboutInfo();

            Assert.Equal(0, invalidResult.CategoriesCount);
        }
    }
}
