namespace LearningCenter.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using Moq;
    using Xunit;

    public class RateServiceTests
    {
        [Fact]
        public async Task WhenUserVotesTwoTimesOnlyOneVoteShouldBeCountedAndOnlyLastRateShouldBeChecked()
        {
            var ratings = new List<Rating>();
            var courses = new List<Course>();

            var mockRatingsRepo = new Mock<IRepository<Rating>>();
            mockRatingsRepo.Setup(x => x.All()).Returns(ratings.AsQueryable());
            mockRatingsRepo.Setup(x => x.AddAsync(It.IsAny<Rating>())).Callback((Rating rate) => ratings.Add(rate));

            var mockCoursesRepo = new Mock<IDeletableEntityRepository<Course>>();
            mockCoursesRepo.Setup(x => x.All()).Returns(courses.AsQueryable());
            mockCoursesRepo.Setup(x => x.AddAsync(It.IsAny<Course>())).Callback((Course course) => courses.Add(course));

            var service = new RateService(mockRatingsRepo.Object, mockCoursesRepo.Object);

            await service.SetRateAsync("1", 1, 5);
            await service.SetRateAsync("1", 1, 3);
            await service.SetRateAsync("1", 1, 1);
            await service.SetRateAsync("1", 1, 3);

            Assert.Equal(1, (int)ratings.Count);
            Assert.Equal(3, ratings.FirstOrDefault().Value);

            await service.SetRateAsync("2", 1, 1);
            await service.SetRateAsync("3", 1, 3);
            Assert.Equal(3, ratings.Count);
        }

        [Fact]
        public async Task MethodReturnsCorrectAverageRate()
        {
            var ratings = new List<Rating>();
            var courses = new List<Course>();

            var mockRatingsRepo = new Mock<IRepository<Rating>>();
            mockRatingsRepo.Setup(x => x.All()).Returns(ratings.AsQueryable());
            mockRatingsRepo.Setup(x => x.AddAsync(It.IsAny<Rating>())).Callback((Rating rate) => ratings.Add(rate));

            var mockCoursesRepo = new Mock<IDeletableEntityRepository<Course>>();
            mockCoursesRepo.Setup(x => x.All()).Returns(courses.AsQueryable());
            mockCoursesRepo.Setup(x => x.AddAsync(It.IsAny<Course>())).Callback((Course course) => courses.Add(course));

            courses.Add(new Course { Id = 1, Ratings = ratings });

            var service = new RateService(mockRatingsRepo.Object, mockCoursesRepo.Object);

            await service.SetRateAsync("1", 1, 2);
            await service.SetRateAsync("2", 1, 2);
            await service.SetRateAsync("3", 1, 4);
            await service.SetRateAsync("4", 1, 4);
            await service.SetRateAsync("1", 1, 5);
            await service.SetRateAsync("1", 1, 2);

            var actualAverageValue = service.GetAverageRating(1);
            Assert.Equal(3, actualAverageValue);

            await service.SetRateAsync("5", 1, 4);

            var newActualAverageValue = service.GetAverageRating(1);
            Assert.Equal(3.2, newActualAverageValue);
        }

        [Fact]
        public async Task GetsRatesCountMustWorkCorrectly()
        {
            var ratings = new List<Rating>();
            var courses = new List<Course>();

            var mockRatingsRepo = new Mock<IRepository<Rating>>();
            mockRatingsRepo.Setup(x => x.All()).Returns(ratings.AsQueryable());
            mockRatingsRepo.Setup(x => x.AddAsync(It.IsAny<Rating>())).Callback((Rating rate) => ratings.Add(rate));

            var mockCoursesRepo = new Mock<IDeletableEntityRepository<Course>>();
            mockCoursesRepo.Setup(x => x.All()).Returns(courses.AsQueryable());
            mockCoursesRepo.Setup(x => x.AddAsync(It.IsAny<Course>())).Callback((Course course) => courses.Add(course));

            courses.Add(new Course { Id = 1, Ratings = ratings });

            var service = new RateService(mockRatingsRepo.Object, mockCoursesRepo.Object);

            Assert.Equal(0, service.GetRatesCount(1));

            await service.SetRateAsync("1", 1, 2);
            await service.SetRateAsync("2", 1, 2);
            await service.SetRateAsync("3", 1, 4);
            await service.SetRateAsync("4", 1, 4);

            Assert.Equal(4, service.GetRatesCount(1));

            await service.SetRateAsync("1", 1, 5);
            await service.SetRateAsync("3", 2, 5);
            Assert.Equal(4, service.GetRatesCount(1));
        }

        [Fact]
        public async Task GetRatingValueByUserAndCourseIdMustReturnRightValueIfParametersAreValidAndReturnsNullWithInvalidParameters()
        {
            var ratings = new List<Rating>();
            var courses = new List<Course>();

            var mockRatingsRepo = new Mock<IRepository<Rating>>();
            mockRatingsRepo.Setup(x => x.All()).Returns(ratings.AsQueryable());
            mockRatingsRepo.Setup(x => x.AddAsync(It.IsAny<Rating>())).Callback((Rating rate) => ratings.Add(rate));

            var mockCoursesRepo = new Mock<IDeletableEntityRepository<Course>>();
            mockCoursesRepo.Setup(x => x.All()).Returns(courses.AsQueryable());
            mockCoursesRepo.Setup(x => x.AddAsync(It.IsAny<Course>())).Callback((Course course) => courses.Add(course));

            courses.Add(new Course { Id = 1, Ratings = ratings });

            var service = new RateService(mockRatingsRepo.Object, mockCoursesRepo.Object);
            await service.SetRateAsync("1", 1, 2);
            await service.SetRateAsync("2", 1, 2);
            await service.SetRateAsync("3", 1, 4);
            await service.SetRateAsync("4", 1, 4);

            // With correct Ids for Course And User
            var actualResult = service.GetRateByUserAndCourse(1, "2");
            Assert.Equal(2, actualResult);

            // MustReturnNullWithIncorrectParameters
            actualResult = service.GetRateByUserAndCourse(2, "2");
            Assert.Null(actualResult);
            actualResult = service.GetRateByUserAndCourse(1, "5");
            Assert.Null(actualResult);
        }
    }
}
