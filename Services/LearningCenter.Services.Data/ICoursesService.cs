namespace LearningCenter.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LearningCenter.Web.ViewModels.Courses;

    public interface ICoursesService
    {
        Task<int> AddCourseAsync(CreateCourseInputModel inputModel, string thumbnailUrl, string authorId);

        Task<bool> DeleteAsync(int courseId, string userId);

        T GetCourse<T>(int id);

        Task AddCourseToBagAsync(int courseId, string userId);

        Task RemoveCourseFromBag(int courseId, string userId);

        int GetCountByCategoryId(int categoryId);

        int GetCountBySubCategoryId(int subcategoryId);

        int GetCountByAuthorId(string authorId);

        Task<IEnumerable<T>> GetAllByCategoryId<T>(int categoryId, int page, int itemsPerPage);
    }
}
