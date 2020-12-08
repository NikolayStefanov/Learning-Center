namespace LearningCenter.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IDeletableEntityRepository<SubCategory> subcategoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository, IDeletableEntityRepository<SubCategory> subcategoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.subcategoryRepository = subcategoryRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.categoryRepository.All().To<T>().ToList();
        }

        public IEnumerable<SelectListItem> GetAllAsSelectListItems()
        {
            return this.categoryRepository.AllAsNoTracking().Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Title }).ToList();
        }

        public IEnumerable<SelectListItem> GetSubcategoriesAsSelectListItems(int categoryId)
        {
            var allSubcategories = this.subcategoryRepository.All().Where(x=> x.CategoryId == categoryId).Select(x=> new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Title,
            }).ToList();
            return allSubcategories;
        }

        public T GetTatgerCategory<T>(int id)
        {
            return this.categoryRepository.All().Where(c => c.Id == id).To<T>().FirstOrDefault();
        }
    }
}
