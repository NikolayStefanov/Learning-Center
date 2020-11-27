namespace LearningCenter.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.categoryRepository.All().To<T>().ToList();
        }

        public IEnumerable<SelectListItem> GetAllAsSelectListItems()
        {
            return this.categoryRepository.AllAsNoTracking().Select(c => new SelectListItem() { Value = c.Id.ToString(), Text = c.Title }).ToList();
        }

        public IEnumerable<SelectListItem> GetSubcategoriesAsSelectListItems(int categoryId, string categoryValue)
        {
            var category = this.categoryRepository.AllAsNoTracking().Where(c => c.Id == categoryId).FirstOrDefault();
            return category.SubCategories.Select(sc => new SelectListItem() { Value = sc.Id.ToString(), Text = sc.Title }).ToList();
        }

        public T GetTatgerCategory<T>(int id)
        {
            return this.categoryRepository.AllAsNoTracking().Where(c => c.Id == id).To<T>().FirstOrDefault();
        }
    }
}
