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
            mockCourseRepo.Setup(x => x.AllAsNoTracking()).Returns(this.courses.AsQueryable());
            mockCourseRepo.Setup(x => x.AddAsync(It.IsAny<Course>())).Callback((Course course) => this.courses.Add(course));
            mockCourseRepo.Setup(x => x.Delete(It.IsAny<Course>())).Callback((Course course) => this.courses.Remove(course));

            var mockUsersRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockUsersRepo.Setup(x => x.All()).Returns(this.users.AsQueryable());

            var mockUserCoursesRepo = new Mock<IDeletableEntityRepository<UserCourses>>();
            mockUserCoursesRepo.Setup(x => x.All()).Returns(this.userCourses.AsQueryable());
            mockUserCoursesRepo.Setup(x => x.AllWithDeleted()).Returns(this.userCourses.AsQueryable());
            mockUserCoursesRepo.Setup(x => x.AddAsync(It.IsAny<UserCourses>())).Callback((UserCourses userCourse) => this.userCourses.Add(userCourse));
            mockUserCoursesRepo.Setup(x => x.Delete(It.IsAny<UserCourses>())).Callback((UserCourses userCourse) => this.userCourses.Remove(userCourse));

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

        [Fact]
        public async Task DeleteAsyncMustReturnTrueIfEntityIsDeletedAndFalseIfEntityIsNotDeleted()
        {
            this.courses.AddRange(new List<Course>
            {
                new Course{ Id = 1, AuthorId = "1" },
                new Course { Id = 2, AuthorId = "1" },
                new Course { Id = 3, AuthorId = "2" },
            });

            Assert.Equal(3, this.courses.Count());
            var trueResult = await this.service.DeleteAsync(1, "1");
            var falseResult = await this.service.DeleteAsync(3, "1");
            Assert.True(trueResult);
            Assert.False(falseResult);
            Assert.Equal(2, this.courses.Count());
        }

        [Fact]
        public void GetCountByAuthorIdMustReturnCountOfUserCourses()
        {
            this.courses.AddRange(new List<Course>
            {
                new Course { Id = 1, AuthorId = "1" },
                new Course { Id = 2, AuthorId = "1" },
                new Course { Id = 3, AuthorId = "2" },
            });

            var coursesOfAuthorWithIdOne = this.service.GetCountByAuthorId("1");
            var coursesOfAuthorWithIdTwo = this.service.GetCountByAuthorId("2");
            var coursesOfAuthorWithNoCourses = this.service.GetCountByAuthorId("3");
            Assert.Equal(2, coursesOfAuthorWithIdOne);
            Assert.Equal(1, coursesOfAuthorWithIdTwo);
            Assert.Equal(0, coursesOfAuthorWithNoCourses);
        }

        [Fact]
        public void GetCountByCategoryIdMustReturnCorrectCountOfCourses()
        {
            this.courses.AddRange(new List<Course>
            {
                new Course { Id = 1, CategoryId = 1 },
                new Course { Id = 2, CategoryId = 1 },
                new Course { Id = 3, CategoryId = 2 },
            });

            var coursesOfCategoryWithIdOne = this.service.GetCountByCategoryId(1);
            var coursesOfCategoryWithIdTwo = this.service.GetCountByCategoryId(2);
            var coursesOfCategoryWithNoCourses = this.service.GetCountByCategoryId(3);
            Assert.Equal(2, coursesOfCategoryWithIdOne);
            Assert.Equal(1, coursesOfCategoryWithIdTwo);
            Assert.Equal(0, coursesOfCategoryWithNoCourses);
        }

        [Fact]
        public void GetCountBySubCategoryIdMustReturnCorrectCountOfCourses()
        {
            this.courses.AddRange(new List<Course>
            {
                new Course { Id = 1, SubCategoryId = 1 },
                new Course { Id = 2, SubCategoryId = 1 },
                new Course { Id = 3, SubCategoryId = 2 },
            });

            var coursesOfSubCategoryWithIdOne = this.service.GetCountBySubCategoryId(1);
            var coursesOfSubCategoryWithIdTwo = this.service.GetCountBySubCategoryId(2);
            var coursesOfSubCategoryWithNoCourses = this.service.GetCountBySubCategoryId(3);
            Assert.Equal(2, coursesOfSubCategoryWithIdOne);
            Assert.Equal(1, coursesOfSubCategoryWithIdTwo);
            Assert.Equal(0, coursesOfSubCategoryWithNoCourses);
        }

        [Fact]
        public async Task RemoveCourseFromBagMustRemoveFromCourseBagCorrectly()
        {
            this.userCourses.AddRange(new List<UserCourses>
            {
                new UserCourses{ CourseId = 1, StudentId = "1" },
                new UserCourses{ CourseId = 1, StudentId = "2" },
                new UserCourses{ CourseId = 3, StudentId = "1" },
                new UserCourses{ CourseId = 3, StudentId = "2" },
                new UserCourses{ CourseId = 5, StudentId = "1" },
            });

            Assert.Equal(5, this.userCourses.Count());

            await this.service.RemoveCourseFromBag(1, "1");
            Assert.Equal(4, this.userCourses.Count());
            Assert.Equal("2", this.userCourses.First().StudentId);

            await this.service.RemoveCourseFromBag(5, "2");
            Assert.Equal(4, this.userCourses.Count());
        }

        [Fact]
        public void GetCourseByIdMustReturnReadyToUserViewModel()
        {
            var author = new ApplicationUser
            {
                FirstName = "Nikolay",
                LastName = "Stefanov",
            };
            var courseRatings = new List<Rating>
            {
                new Rating{ Value = 5},
                new Rating{ Value = 3},
            };
            this.courses.AddRange(new List<Course>
            {
                new Course{ Id = 1, Title = "CourseTitle", Price = 25.50M, Description = "Course Description", Thumbnail = new CourseThumbnail{Url = "thumbnail url" }, Ratings = courseRatings,  AuthorId = "1", Author = author },
            });

            var resultObj = this.service.GetCourse<CourseViewModel>(1);

            Assert.Equal("Nikolay Stefanov", resultObj.AuthorName);
            Assert.Equal(4, resultObj.AverageRating);

            var invalidCourseIdObj = this.service.GetCourse<CourseViewModel>(2);
            Assert.Null(invalidCourseIdObj);
        }
    }
}
