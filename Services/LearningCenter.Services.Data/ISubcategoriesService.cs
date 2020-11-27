namespace LearningCenter.Services.Data
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public interface ISubcategoriesService
    {
        IEnumerable<SelectListItem> GetAllAsSelectListItems();
    }
}
