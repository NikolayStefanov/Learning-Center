namespace LearningCenter.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LearningCenter.Web.ViewModels.Lectures;

    public interface ILecturesService
    {
        Task<int> AddAsync(LectureInputModel inputModel, string userId, string thumbnailUrl, IEnumerable<string> additionalMaterials);
    }
}
