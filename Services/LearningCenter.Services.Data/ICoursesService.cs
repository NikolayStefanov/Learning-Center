namespace LearningCenter.Services.Data
{
    using System.Threading.Tasks;

    using LearningCenter.Web.ViewModels.Courses;

    public interface ICoursesService
    {
        Task<int> AddCourseAsync(CreateCourseInputModel inputModel, string thumbnailUrl, string authorId);

        Task<bool> DeleteAsync(int courseId, string userId);

        T GetCourse<T>(int id);

        Task AddCourseToBagAsync(int courseId, string userId);

        Task RemoveCourseFromBag(int courseId, string userId);
    }
}
