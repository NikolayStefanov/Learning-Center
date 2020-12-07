namespace LearningCenter.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class SubcategoriesService : ISubcategoriesService
    {
        private readonly IDeletableEntityRepository<SubCategory> subcategoryRepo;

        public SubcategoriesService(IDeletableEntityRepository<SubCategory> subcategoryRepo)
        {
            this.subcategoryRepo = subcategoryRepo;
        }

        public IEnumerable<SelectListItem> GetAllAsSelectListItems()
        {
            var subcategories = this.subcategoryRepo
                .AllAsNoTracking()
                .Select(sc => new SelectListItem()
                {
                    Value = sc.Id.ToString(),
                    Text = sc.Title,
                })
                .ToList();
            return subcategories;
        }

        public T GetSubcategory<T>(int id)
        {
            var targetSubcategory = this.subcategoryRepo.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
            return targetSubcategory;
        }
    }
}
