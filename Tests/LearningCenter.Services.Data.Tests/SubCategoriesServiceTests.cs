namespace LearningCenter.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using LearningCenter.Web.ViewModels;
    using LearningCenter.Web.ViewModels.SubCategories;
    using Moq;
    using Xunit;

    public class SubCategoriesServiceTests
    {
        private List<SubCategory> subcategories;
        private Mock<IDeletableEntityRepository<SubCategory>> mockSubcategoriesRepo;
        private SubcategoriesService service;

        public SubCategoriesServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly, Assembly.Load("LearningCenter.Services.Data.Tests"));
            this.subcategories = new List<SubCategory>();
            this.mockSubcategoriesRepo = new Mock<IDeletableEntityRepository<SubCategory>>();
            this.mockSubcategoriesRepo.Setup(x => x.All()).Returns(this.subcategories.AsQueryable());
            this.mockSubcategoriesRepo.Setup(x => x.AllAsNoTracking()).Returns(this.subcategories.AsQueryable());

            this.service = new SubcategoriesService(this.mockSubcategoriesRepo.Object);

            var coursesToAdd = new List<Course>
            {
                new Course(),
                new Course(),
                new Course(),
            };

            this.subcategories.AddRange(new List<SubCategory>
            {
                new SubCategory { Id = 1, Title = "FirstSubCategoryTitle", Courses = coursesToAdd },
                new SubCategory { Id = 2, Title = "SecondSubCategoryTitle", Courses = coursesToAdd },
            });
        }

        [Fact]
        public void GetSubcategoryMustWorkCorrectlyAndReturnWantedTemplateModel()
        {
            var resultObj = this.service.GetSubcategory<SubCategoryViewModel>(2);
            Assert.Equal(2, resultObj.Id);
            Assert.Equal("SecondSubCategoryTitle", resultObj.Title);

            var invalidIdObj = this.service.GetSubcategory<SubCategoryViewModel>(3);
            Assert.Null(invalidIdObj);
        }

        [Fact]
        public void GetAllAsSelectListItemsMustReturnCorrectlySelectListItems()
        {
            var selectListItem = this.service.GetAllAsSelectListItems();

            Assert.Equal(2, selectListItem.Count());
            Assert.Equal("1", selectListItem.First().Value);
            Assert.Equal("2", selectListItem.Last().Value);
            Assert.Equal("FirstSubCategoryTitle", selectListItem.First().Text);
            Assert.Equal("SecondSubCategoryTitle", selectListItem.Last().Text);

            this.subcategories.Clear();
            var invalidListItem = this.service.GetAllAsSelectListItems();
            Assert.Equal(0, (int)invalidListItem.Count());
        }
    }
}
