namespace LearningCenter.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using Moq;
    using Xunit;

    public class LanguagesServiceTests
    {
        [Fact]
        public void GetAllLanguagesAsSelectListItemsShouldWorkCorrectly()
        {
            var languages = new List<Language>
            {
                new Language { Name = "Bulgarian", Id = 1 },
                new Language { Name = "English", Id = 2 },
                new Language { Name = "Spanish", Id = 3 },
            };
            var mockLanguagesRepository = new Mock<IDeletableEntityRepository<Language>>();
            mockLanguagesRepository.Setup(x => x.AllAsNoTracking()).Returns(languages.AsQueryable());

            var service = new LanguagesService(mockLanguagesRepository.Object);

            var resultListItems = service.GetAllAsSelectListItems();
            Assert.Equal(3, resultListItems.Count());
            Assert.Equal("Bulgarian", resultListItems.First().Text);
            Assert.Equal("Spanish", resultListItems.Last().Text);
        }
    }
}
