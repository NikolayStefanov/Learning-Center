namespace LearningCenter.Services.Data
{
    using System.Threading.Tasks;

    using LearningCenter.Web.ViewModels.Reviews;

    public interface IReviewsService
    {
        Task<int> AddAsync(ReviewInputModel inputModel);

        T GetReviewById<T>(int id);
    }
}
