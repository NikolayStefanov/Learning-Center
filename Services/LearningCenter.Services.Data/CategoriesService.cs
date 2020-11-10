using LearningCenter.Data.Common.Repositories;
using LearningCenter.Data.Models;
using LearningCenter.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningCenter.Services.Data
{
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
    }
}
