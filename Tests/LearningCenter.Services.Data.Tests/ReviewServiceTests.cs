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
        private IList<ApplicationUser> users;
        private IList<Review> reviews;
        private ReviewsService service;

        public ReviewServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly, Assembly.Load("LearningCenter.Services.Data.Tests"));

            this.users = new List<ApplicationUser>();
            this.reviews = new List<Review>();

            var mockUsersRepo = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockUsersRepo.Setup(x => x.All()).Returns(this.users.AsQueryable());

            var mockReviewsRepo = new Mock<IDeletableEntityRepository<Review>>();
            mockReviewsRepo.Setup(x => x.All()).Returns(this.reviews.AsQueryable());
            mockReviewsRepo.Setup(x => x.AddAsync(It.IsAny<Review>())).Callback((Review review) => this.reviews.Add(review));

            this.service = new ReviewsService(mockReviewsRepo.Object, mockUsersRepo.Object);
        }

        [Fact]
        public async Task AddAsyncMustAddReview()
        {
            this.users.Add(new ApplicationUser { Id = "1", Email = "email" });
            var inputModel = new ReviewInputModel { AuthorEmail = "email", Content = "reviewContent", CourseId = 1 };

            await this.service.AddAsync(inputModel);

            Assert.Equal(1, (int)this.reviews.Count);
            Assert.Equal("reviewContent", this.reviews.First().Content);
        }

        [Fact]
        public void GetReviewByIdMustReturnCorrectModel()
        {
            this.users.Add(new ApplicationUser { Id = "Nikolay", FirstName = "Nikolay", LastName = "Stefanov", ProfilePicture = new ProfilePicture { Url = "picUrl" } });
            this.reviews.Add(new Review { Author = this.users.First(), AuthorId = "Nikolay", Content = "Review Content", Id = 1 });

            var actualResult = this.service.GetReviewById<ReviewViewModel>(1);

            Assert.Equal("Nikolay", actualResult.AuthorId);
            Assert.Equal("Nikolay Stefanov", actualResult.AuthorFullName);
            Assert.Equal("picUrl", actualResult.AuthorProfilePictureUrl);
        }
    }
}
