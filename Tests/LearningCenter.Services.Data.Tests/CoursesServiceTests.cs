namespace LearningCenter.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using LearningCenter.Web.ViewModels;
    using LearningCenter.Web.ViewModels.Courses;
    using Moq;
    using Xunit;

    public class CoursesServiceTests
    {
        private List<Course> courses;
        private List<UserCourses> userCourses;
        private List<ApplicationUser> users;
        private CoursesService service;

        public CoursesServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly, Assembly.Load("LearningCenter.Services.Data.Tests"));
            this.courses = new List<Course>();
            this.userCourses = new List<UserCourses>();

            this.users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", Lecturer = new Lecturer { Id = "1", Courses = new List<Course>() }, Courses = this.userCourses.ToList() },
            };

            var mockCourseRepo = new Mock<IDeletableEntityRepository<Course>>();
            mockCourseRepo.Setup(x => x.All()).Returns(this.courses.AsQueryable());
            mockCourseRepo.Setup(x => x.AddAsync(It.IsAny<Course>())).Callback((Course course) => this.courses.Add(course));

            var mockUsersRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockUsersRepo.Setup(x => x.All()).Returns(this.users.AsQueryable());

            var mockUserCoursesRepo = new Mock<IDeletableEntityRepository<UserCourses>>();
            mockUserCoursesRepo.Setup(x => x.AllWithDeleted()).Returns(this.userCourses.AsQueryable());
            mockUserCoursesRepo.Setup(x => x.AddAsync(It.IsAny<UserCourses>())).Callback((UserCourses userCourse) => this.userCourses.Add(userCourse));

            this.service = new CoursesService(mockCourseRepo.Object, mockUsersRepo.Object, mockUserCoursesRepo.Object);
        }

        [Fact]
        public async Task AddCourseAsyncMustAddCourseInDataBase()
        {
            var inputModel = new CreateCourseInputModel
            {
                Title = "CourseTitle",
                Description = "CourseDescription",
                Price = 25.50M,
                CategoryId = 1,
                SubcategoryId = 1,
                LanguageId = 1,
            };
            await this.service.AddCourseAsync(inputModel, "thumbainUrl", "1");

            Assert.Equal(1, (int)this.courses.Count);
            Assert.Equal(1, (int)this.users.First().Lecturer.Courses.Count);
        }

        [Fact]
        public async Task AddCourseToBagAsyncMustAddInUserCoursesIfThereIsntTargetUserCoursOrMustSetIsDeletedToFalseIfUserCourseEntityIsDeleted()
        {
            this.courses.Add(new Course
            {
                Id = 1,
            });

            await this.service.AddCourseToBagAsync(1, "1");

            Assert.Equal(1, (int)this.userCourses.Count);

            this.userCourses.First().IsDeleted = true;
            await this.service.AddCourseToBagAsync(1, "1");

            Assert.False(this.userCourses.First().IsDeleted);

        }
    }
}
