namespace LearningCenter.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using LearningCenter.Web.ViewModels;
    using LearningCenter.Web.ViewModels.Account;
    using Moq;
    using Xunit;

    public class AccountServiceTests
    {
        private List<ApplicationUser> users;
        private List<Feedback> feedbacks;
        private AccountsService service;

        public AccountServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly, Assembly.Load("LearningCenter.Services.Data.Tests"));

            this.users = new List<ApplicationUser>();
            this.feedbacks = new List<Feedback>();

            var mockUsersRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            mockUsersRepository.Setup(x => x.All()).Returns(this.users.AsQueryable());
            mockUsersRepository.Setup(x => x.AllAsNoTracking()).Returns(this.users.AsQueryable());
            var mockFeedbacksRepository = new Mock<IDeletableEntityRepository<Feedback>>();
            mockFeedbacksRepository.Setup(x => x.All()).Returns(this.feedbacks.AsQueryable());
            mockFeedbacksRepository.Setup(x => x.AddAsync(It.IsAny<Feedback>())).Callback((Feedback feedback) => this.feedbacks.Add(feedback));

            this.service = new AccountsService(mockUsersRepository.Object, mockFeedbacksRepository.Object);

            this.users.AddRange(new List<ApplicationUser>
            {
                new ApplicationUser { Id = "FirstUserId", FirstName = "Nikolay", LastName = "Stefanov", BirthDate = new DateTime(1997, 03, 17), Email = "userEmail@abv.bg", ProfilePicture = new ProfilePicture { Url = "UserProfilePictureUrl" }, Lecturer = new Lecturer { Id = "lecturerId" } },
                new ApplicationUser { Id = "SomeId", FirstName = "Plamen", LastName = "Stefanov", BirthDate = new DateTime(1995, 10, 11), Email = "user2Email@abv.bg", ProfilePicture = new ProfilePicture { Url = "User2ProfilePictureUrl" } },
            });
        }

        [Fact]
        public void GetLecturerByIdMustReturnCorrectModelIfIdExistsAndNullIfNot()
        {
            var resultObj = this.service.GetLecturerById<ProfileViewModel>("FirstUserId");
            Assert.Equal("Nikolay Stefanov", resultObj.FullName);
            Assert.Equal("FirstUserId", resultObj.Id);

            var invalidIdPassed = this.service.GetLecturerById<ProfileViewModel>("invalidId");
            Assert.Null(invalidIdPassed);
        }

        [Fact]
        public void GetFeedbackByIdMustReturnCorrectModelIfExistsAndNullIfNot()
        {
            this.feedbacks.AddRange(new List<Feedback>
            {
                new Feedback { Id = 1, Author = this.users.First(), Content = "FeebackContent" },
            });
            var resultFeedback = this.service.GetFeedbackById<FeedbackViewModel>(1);
            Assert.Equal("FeebackContent", resultFeedback.Content);
            Assert.Equal("Nikolay Stefanov", resultFeedback.AuthorFullName);
            Assert.Equal("UserProfilePictureUrl", resultFeedback.AuthorProfilePictureUrl);

            var invalidIdPassed = this.service.GetFeedbackById<FeedbackViewModel>(2);
            Assert.Null(invalidIdPassed);
        }

        [Fact]
        public async Task AddFeedbackAsyncMustWorkCorrectly()
        {
            var inputModel = new FeedbackInputModel
            {
                AuthorEmail = "user2Email@abv.bg",
                Content = "Great Feedback Content",
                LecturerId = "FirstUserId",
            };

            await this.service.AddFeedbackAsync(inputModel);

            Assert.Equal(1, (int)this.feedbacks.Count());
            await this.service.AddFeedbackAsync(inputModel);
            await this.service.AddFeedbackAsync(inputModel);
            Assert.Equal(3, (int)this.feedbacks.Count());
        }
    }
}
