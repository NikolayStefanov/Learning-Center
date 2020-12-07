namespace LearningCenter.Web.Controllers
{
    using LearningCenter.Services.Data;
    using LearningCenter.Web.ViewModels.SubCategories;
    using Microsoft.AspNetCore.Mvc;

    public class SubcategoriesController : BaseController
    {
        private readonly ISubcategoriesService subcategoriesService;

        public SubcategoriesController(ISubcategoriesService subcategoriesService)
        {
            this.subcategoriesService = subcategoriesService;
        }

        public IActionResult GetSubcategory(int id)
        {
            var viewModel = this.subcategoriesService.GetSubcategory<SubCategoryViewModel>(id);

            return this.View(viewModel);
        }
    }
}
