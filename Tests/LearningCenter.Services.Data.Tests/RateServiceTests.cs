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
        private IList<Rating> ratings;
        private IList<Course> courses;
        private RateService service;

        public RateServiceTests()
        {
            this.ratings = new List<Rating>();
            this.courses = new List<Course>();

            var mockRatingsRepo = new Mock<IRepository<Rating>>();
            mockRatingsRepo.Setup(x => x.All()).Returns(this.ratings.AsQueryable());
            mockRatingsRepo.Setup(x => x.AddAsync(It.IsAny<Rating>())).Callback((Rating rate) => this.ratings.Add(rate));

            var mockCoursesRepo = new Mock<IDeletableEntityRepository<Course>>();
            mockCoursesRepo.Setup(x => x.All()).Returns(this.courses.AsQueryable());
            mockCoursesRepo.Setup(x => x.AddAsync(It.IsAny<Course>())).Callback((Course course) => this.courses.Add(course));

            this.service = new RateService(mockRatingsRepo.Object, mockCoursesRepo.Object);
        }

        [Fact]
        public async Task WhenUserVotesTwoTimesOnlyOneVoteShouldBeCountedAndOnlyLastRateShouldBeChecked()
        {
            await this.service.SetRateAsync("1", 1, 5);
            await this.service.SetRateAsync("1", 1, 3);
            await this.service.SetRateAsync("1", 1, 1);
            await this.service.SetRateAsync("1", 1, 3);

            Assert.Equal(1, (int)this.ratings.Count());
            Assert.Equal(3, this.ratings.FirstOrDefault().Value);

            await this.service.SetRateAsync("2", 1, 1);
            await this.service.SetRateAsync("3", 1, 3);
            Assert.Equal(3, this.ratings.Count());
        }

        [Fact]
        public async Task MethodReturnsCorrectAverageRate()
        {
            this.courses.Add(new Course { Id = 1, Ratings = this.ratings });

            await this.service.SetRateAsync("1", 1, 2);
            await this.service.SetRateAsync("2", 1, 2);
            await this.service.SetRateAsync("3", 1, 4);
            await this.service.SetRateAsync("4", 1, 4);
            await this.service.SetRateAsync("1", 1, 5);
            await this.service.SetRateAsync("1", 1, 2);

            var actualAverageValue = this.service.GetAverageRating(1);
            Assert.Equal(3, actualAverageValue);

            await this.service.SetRateAsync("5", 1, 4);

            var newActualAverageValue = this.service.GetAverageRating(1);
            Assert.Equal(3.2, newActualAverageValue);
        }

        [Fact]
        public async Task GetsRatesCountMustWorkCorrectly()
        {
            this.courses.Add(new Course { Id = 1, Ratings = this.ratings });

            Assert.Equal(0, this.service.GetRatesCount(1));

            await this.service.SetRateAsync("1", 1, 2);
            await this.service.SetRateAsync("2", 1, 2);
            await this.service.SetRateAsync("3", 1, 4);
            await this.service.SetRateAsync("4", 1, 4);

            Assert.Equal(4, this.service.GetRatesCount(1));

            await this.service.SetRateAsync("1", 1, 5);
            await this.service.SetRateAsync("3", 2, 5);
            Assert.Equal(4, this.service.GetRatesCount(1));
        }

        [Fact]
        public async Task GetRatingValueByUserAndCourseIdMustReturnRightValueIfParametersAreValidAndReturnsNullWithInvalidParameters()
        {
            this.courses.Add(new Course { Id = 1, Ratings = this.ratings });

            await this.service.SetRateAsync("1", 1, 2);
            await this.service.SetRateAsync("2", 1, 2);
            await this.service.SetRateAsync("3", 1, 4);
            await this.service.SetRateAsync("4", 1, 4);

            // With correct Ids for Course And User
            var actualResult = this.service.GetRateByUserAndCourse(1, "2");
            Assert.Equal(2, actualResult);

            // MustReturnNullWithIncorrectParameters
            actualResult = this.service.GetRateByUserAndCourse(2, "2");
            Assert.Null(actualResult);
            actualResult = this.service.GetRateByUserAndCourse(1, "5");
            Assert.Null(actualResult);
        }
    }
}
