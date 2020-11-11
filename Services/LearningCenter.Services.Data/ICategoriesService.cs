namespace LearningCenter.Services.Data
{
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>();

        T GetTatgerCategory<T>(int id);
    }
}
