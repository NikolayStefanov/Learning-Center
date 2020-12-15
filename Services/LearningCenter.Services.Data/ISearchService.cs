namespace LearningCenter.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISearchService
    {
        Task<IEnumerable<T>> GetSearchedCoursesAsync<T>(string searchedWord);
    }
}
