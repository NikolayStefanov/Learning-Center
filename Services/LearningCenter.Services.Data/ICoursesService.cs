namespace LearningCenter.Services.Data
{
    using System.Threading.Tasks;

    using LearningCenter.Web.ViewModels.Courses;

    public interface ICoursesService
    {
        Task<int> AddCourseAsync(CreateCourseInputModel inputModel, string thumbnailUrl, string authorId);
    }
}
