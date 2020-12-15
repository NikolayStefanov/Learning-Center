using LearningCenter.Data.Common.Repositories;
using LearningCenter.Data.Models;
using LearningCenter.Services.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCenter.Services.Data
{
    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<Course> coursesRepository;

        public SearchService(IDeletableEntityRepository<Course> coursesRepository)
        {
            this.coursesRepository = coursesRepository;
        }

        public async Task<IEnumerable<T>> GetSearchedCoursesAsync<T>(string searchedWord)
        {
            var normalizedWord = searchedWord.ToLower();
            var courses = await this.coursesRepository.All().Where(x => x.Title.ToLower().Contains(normalizedWord)).To<T>().ToListAsync();

            if (courses == null || !courses.Any())
            {
                courses = await this.coursesRepository.All().Where(x => x.Description.ToLower().Contains(normalizedWord)).To<T>().ToListAsync();
            }

            return courses;
        }
    }
}
