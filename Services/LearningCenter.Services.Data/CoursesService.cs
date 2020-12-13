namespace LearningCenter.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using LearningCenter.Web.ViewModels.Courses;
    using Microsoft.EntityFrameworkCore;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<UserCourses> userCoursesRepository;

        public CoursesService(IDeletableEntityRepository<Course> courseRepository, IDeletableEntityRepository<ApplicationUser> userRepository, IDeletableEntityRepository<UserCourses> userCoursesRepository)
        {
            this.courseRepository = courseRepository;
            this.userRepository = userRepository;
            this.userCoursesRepository = userCoursesRepository;
        }

        public async Task<int> AddCourseAsync(CreateCourseInputModel inputModel, string thumbnailUrl, string authorId)
        {
            var thumbnail = new CourseThumbnail() { Url = thumbnailUrl };

            var newCourse = new Course()
            {
                Title = inputModel.Title,
                Price = inputModel.Price,
                Thumbnail = thumbnail,
                CategoryId = inputModel.CategoryId,
                LanguageId = inputModel.LanguageId,
                SubCategoryId = inputModel.SubcategoryId,
                Description = inputModel.Description,
                AuthorId = authorId,
            };
            this.userRepository.All().Include(x => x.Lecturer).FirstOrDefault(x => x.Id == authorId).Lecturer.Courses.Add(newCourse);

            await this.courseRepository.AddAsync(newCourse);
            await this.courseRepository.SaveChangesAsync();
            return newCourse.Id;
        }

        public async Task AddCourseToBagAsync(int courseId, string userId)
        {
            var userCourse = this.userCoursesRepository.AllWithDeleted().FirstOrDefault(x => x.StudentId == userId && x.CourseId == courseId);
            if (userCourse == null)
            {
                await this.userCoursesRepository.AddAsync(new UserCourses { CourseId = courseId, StudentId = userId });
            }
            else
            {
                userCourse.IsDeleted = false;
            }

            await this.userCoursesRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int courseId, string userId)
        {
            var user = this.userRepository.All().Include(u=> u.Lecturer).FirstOrDefault(u => u.Id == userId);
            var course = this.courseRepository.All().FirstOrDefault(x => x.Id == courseId);
            if (userId != course.AuthorId)
            {
                return false;
            }

            user.Lecturer.Courses.Remove(course);
            this.courseRepository.Delete(course);
            await this.courseRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAllByCategoryId<T>(int categoryId, int page, int itemsPerPage)
        {
            var courses = await this.courseRepository.All()
                .Where(x => x.CategoryId == categoryId)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToListAsync();
            return courses;
        }

        public int GetCountByAuthorId(string authorId)
        {
            return this.courseRepository.AllAsNoTracking().Where(x => x.AuthorId == authorId).Count();
        }

        public int GetCountByCategoryId(int categoryId)
        {
            return this.courseRepository.AllAsNoTracking().Where(x => x.CategoryId == categoryId).Count();
        }

        public int GetCountBySubCategoryId(int subcategoryId)
        {
            return this.courseRepository.AllAsNoTracking().Where(x => x.SubCategoryId == subcategoryId).Count();
        }

        public T GetCourse<T>(int id)
        {
            var targetCourse = this.courseRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
            return targetCourse;
        }

        public async Task RemoveCourseFromBag(int courseId, string userId)
        {
            var targetEntity = this.userCoursesRepository.All().FirstOrDefault(x => x.StudentId == userId && x.CourseId == courseId);
            this.userCoursesRepository.Delete(targetEntity);
            await this.userCoursesRepository.SaveChangesAsync();
        }
    }
}
