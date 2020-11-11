namespace LearningCenter.Web.Controllers
{
    using System.Collections.Generic;

    using LearningCenter.Services.Data;
    using LearningCenter.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var categoriesViewModel = new IndexViewModel();
            var categories = this.categoriesService.GetAll<CategoriesViewModel>();
            categoriesViewModel.Categories = categories;

            return this.View(categoriesViewModel);
        }

        public IActionResult TargetCategoryPage(int id)
        {
            var viewModel = this.categoriesService.GetTatgerCategory<TargetCategoryViewModel>(id);
            return this.View(viewModel);
        }
    }
}
