namespace LearningCenter.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using LearningCenter.Web.ViewModels;
    using LearningCenter.Web.ViewModels.Categories;
    using Moq;
    using Xunit;

    public class CategoriesServiceTests
    {
        private List<Category> categories;
        private List<SubCategory> subcategories;
        private CategoriesService service;

        public CategoriesServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly, Assembly.Load("LearningCenter.Services.Data.Tests"));
            this.categories = new List<Category>();
            this.subcategories = new List<SubCategory>();

            var mockCategoriesRepo = new Mock<IDeletableEntityRepository<Category>>();
            mockCategoriesRepo.Setup(x => x.All()).Returns(this.categories.AsQueryable());
            mockCategoriesRepo.Setup(x => x.AllAsNoTracking()).Returns(this.categories.AsQueryable());

            var mockSubCategoriesRepo = new Mock<IDeletableEntityRepository<SubCategory>>();
            mockSubCategoriesRepo.Setup(x => x.All()).Returns(this.subcategories.AsQueryable());

            this.service = new CategoriesService(mockCategoriesRepo.Object, mockSubCategoriesRepo.Object);

            var categoriesToAdd = new List<Category>
            {
                new Category { Id = 1, Title = "Category1", ImageName = "Image1" },
                new Category { Id = 2, Title = "Category2", ImageName = "Image2" },
                new Category { Id = 3, Title = "Category3", ImageName = "Image3" },
                new Category { Id = 4, Title = "Category4", ImageName = "Image4" },
                new Category { Id = 5, Title = "Category5", ImageName = "Image5" },
            };
            this.categories.AddRange(categoriesToAdd);
        }

        [Fact]
        public void GetAllMustReturnAllInGivenViewModel()
        {
            var resultObj = this.service.GetAll<CategoriesViewModel>();

            Assert.Equal(5, resultObj.Count());
            Assert.Equal(5, resultObj.Last().Id);
            Assert.Equal("Category2", resultObj.Skip(1).First().Title);
        }

        [Fact]
        public void GetTargetCategoryByIdMustReturnSearchedCategory()
        {
            var resultCategory = this.service.GetTargetCategory<CategoriesViewModel>(4);

            Assert.Equal(4, resultCategory.Id);
            Assert.Equal("Category4", resultCategory.Title);
            Assert.Equal("Image4", resultCategory.ImageName);

            var invalidIdCategory = this.service.GetTargetCategory<CategoriesViewModel>(7);
            Assert.Null(invalidIdCategory);
        }

        [Fact]
        public void GetCategoriesAsSelectListItemsShouldWorkCorrectly()
        {
            var resultSelectListItemList = this.service.GetAllAsSelectListItems().ToList();

            Assert.Equal(5, resultSelectListItemList.Count());
            for (int i = 0; i < resultSelectListItemList.Count(); i++)
            {
                Assert.Equal($"Category{i + 1}", resultSelectListItemList[i].Text);
            }
        }

        [Fact]
        public void GetSubCategoriesSelectListItemsShouldWorkCorrectly()
        {
            var subcategoriesToAdd = new List<SubCategory>
            {
                new SubCategory { CategoryId = 1, Id = 1, Title = "SubCategory1" },
                new SubCategory { CategoryId = 1, Id = 2, Title = "SubCategory2" },
                new SubCategory { CategoryId = 3, Id = 3, Title = "SubCategory3" },
                new SubCategory { CategoryId = 2, Id = 4, Title = "SubCategory4" },
            };
            this.subcategories.AddRange(subcategoriesToAdd);
            var resultSubcategoriesListItem = this.service.GetSubcategoriesAsSelectListItems(1);

            Assert.Equal(2, resultSubcategoriesListItem.Count());
            Assert.Equal("SubCategory1", resultSubcategoriesListItem.First().Text);
            Assert.Equal("SubCategory2", resultSubcategoriesListItem.Skip(1).First().Text);
        }
    }
}
