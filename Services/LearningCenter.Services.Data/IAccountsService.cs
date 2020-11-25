namespace LearningCenter.Services.Data
{
    public interface IAccountsService
    {
        T GetLecturerById<T>(string id);
    }
}
