namespace LearningCenter.Services.Data
{
    using System.Threading.Tasks;

    using LearningCenter.Web.ViewModels.Account;

    public interface IAccountsService
    {
        T GetLecturerById<T>(string id);

        Task<int> AddFeedbackAsync(FeedbackInputModel inputModel);

        T GetFeedbackById<T>(int id);
    }
}
