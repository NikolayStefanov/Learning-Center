namespace LearningCenter.Services.Data
{
    using System.Collections.Generic;

    public interface ICoursesService
    {
        IEnumerable<T> GetAll<T>();
    }
}
