namespace LearningCenter.Services.Data
{
    using System.Collections.Generic;

    using LearningCenter.Web.ViewModels.About;

    public interface IAboutService
    {
        IEnumerable<T> GetTopLecturers<T>();

        SiteAboutViewModel GetAboutInfo();
    }
}
