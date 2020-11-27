namespace LearningCenter.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Web.ViewModels.Courses;

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
            var lecturerId = this.userRepository.All().FirstOrDefault(x => x.Id == authorId).LecturerId;

            var newCourse = new Course()
            {
                Title = inputModel.Title,
                Price = inputModel.Price,
                Thumbnail = thumbnail,
                CategoryId = inputModel.CategoryId,
                LanguageId = inputModel.LanguageId,
                SubCategoryId = inputModel.SubcategoryId,
                Description = inputModel.Description,
                AuthorId = lecturerId,
            };
            await this.courseRepository.AddAsync(newCourse);
            await this.courseRepository.SaveChangesAsync();
            return newCourse.Id;
        }
    }
}
