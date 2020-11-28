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
                CourseUrl = inputModel.CourseUrl,
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
    }
}
