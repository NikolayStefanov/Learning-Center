namespace LearningCenter.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Web.ViewModels.Courses;
    using Microsoft.EntityFrameworkCore;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public CoursesService(IDeletableEntityRepository<Course> courseRepository, IDeletableEntityRepository<ApplicationUser> userRepository, IDeletableEntityRepository<Lecturer> lecturerRepository)
        {
            this.courseRepository = courseRepository;
            this.userRepository = userRepository;
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
    }
}
