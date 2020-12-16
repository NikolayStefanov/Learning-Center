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
    using LearningCenter.Web.ViewModels.Reviews;
    using Moq;
    using Xunit;

    public class ReviewServiceTests
    {
        public ReviewServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly, Assembly.Load("LearningCenter.Services.Data.Tests"));
        }

        [Fact]
        public async Task AddAsyncMustAddReview()
        {
            var users = new List<ApplicationUser>();
            var reviews = new List<Review>();

            var mockUsersRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockUsersRepo.Setup(x => x.All()).Returns(users.AsQueryable());

            var mockReviewsRepo = new Mock<IDeletableEntityRepository<Review>>();
            mockReviewsRepo.Setup(x => x.AddAsync(It.IsAny<Review>())).Callback((Review review) => reviews.Add(review));

            users.Add(new ApplicationUser { Id = "1", Email = "email" });
            var inputModel = new ReviewInputModel { AuthorEmail = "email", Content = "reviewContent", CourseId = 1 };

            var service = new ReviewsService(mockReviewsRepo.Object, mockUsersRepo.Object);

            await service.AddAsync(inputModel);

            Assert.Equal(1, (int)reviews.Count);
            Assert.Equal("reviewContent", reviews.First().Content);
        }

        [Fact]
        public void GetReviewByIdMustReturnCorrectModel()
        {
            var reviews = new List<Review>();
            var users = new List<ApplicationUser>();

            var mockUsersRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockUsersRepo.Setup(x => x.All()).Returns(users.AsQueryable());

            var mockReviewsRepo = new Mock<IDeletableEntityRepository<Review>>();
            mockReviewsRepo.Setup(x => x.All()).Returns(reviews.AsQueryable());
            mockReviewsRepo.Setup(x => x.AddAsync(It.IsAny<Review>())).Callback((Review review) => reviews.Add(review));

            users.Add(new ApplicationUser { Id = "Nikolay", FirstName = "Nikolay", LastName = "Stefanov", ProfilePicture = new ProfilePicture { Url = "picUrl" } });
            reviews.Add(new Review { Author = users.First(), AuthorId = "Nikolay", Content = "Review Content", Id = 1 });
            var service = new ReviewsService(mockReviewsRepo.Object, mockUsersRepo.Object);
            var actualResult = service.GetReviewById<ReviewViewModel>(1);

            Assert.Equal("Nikolay", actualResult.AuthorId);
            Assert.Equal("Nikolay Stefanov", actualResult.AuthorFullName);
            Assert.Equal("picUrl", actualResult.AuthorProfilePictureUrl);
        }
    }
}
