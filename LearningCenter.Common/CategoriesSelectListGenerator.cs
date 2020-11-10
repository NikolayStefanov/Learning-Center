namespace LearningCenter.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using LearningCenter.Services.Data;
    using LearningCenter.Web.ViewModels.Categories;

    public class CategoriesSelectListGenerator
    {
        public static IEnumerable<SelectListItem> GetAllCategories(
           ICategoriesService categoriesService)
        {
            var categories = categoriesService
                .GetAll<CategoriesViewModel>();

            var postCategoryViewModels = categories
                as CategoriesViewModel[] ?? categories.ToArray();

            // var groups = new List<SelectListGroup>();
            // foreach (var category in postCategoryViewModels)
            // {
            //     groups.Add(new SelectListGroup { Name = category.Name });
            // }
            var result = postCategoryViewModels.Select(x => new SelectListItem
            {
                Value = x.Title,
            });

            return result;
        }
    }
}
