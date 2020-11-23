namespace LearningCenter.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Data.Models.Enums;
    using LearningCenter.Services.Mapping;
    using LearningCenter.Web.ViewModels.About;

    public class AboutService : IAboutService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> lecturersRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public AboutService(
            IDeletableEntityRepository<ApplicationUser> lecturersRepository,
            IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.lecturersRepository = lecturersRepository;
            this.categoriesRepository = categoriesRepository;
        }

        public SiteAboutViewModel GetAboutInfo()
        {
            var categoriesCount = this.categoriesRepository.AllAsNoTracking().Count();
            var subcategoriesCount = this.categoriesRepository.AllAsNoTracking().SelectMany(x => x.SubCategories).Count();
            var coursesCount = this.categoriesRepository.AllAsNoTracking().SelectMany(x => x.SubCategories.SelectMany(y => y.Courses)).Count();
            var lecturersCount = this.lecturersRepository.AllAsNoTracking().Count();
            var lecturesCount = this.lecturersRepository.AllAsNoTracking().SelectMany(x => x.Lectures).Count();
            return new SiteAboutViewModel
            {
                CategoriesCount = categoriesCount,
                SubCategoriesCount = subcategoriesCount,
                CoursesCount = coursesCount,
                LecturersCount = lecturersCount,
                LecturesCount = lecturesCount,
            };
        }

        public IEnumerable<T> GetTopLecturers<T>()
        {
            var topLecturers = this.lecturersRepository.All()
                .Where(x => x.UserType == UserTypeEnum.Lecturer)
                .OrderByDescending(x => x.Lectures.Count)
                .To<T>()
                .ToList();
            return topLecturers;
        }
    }
}
