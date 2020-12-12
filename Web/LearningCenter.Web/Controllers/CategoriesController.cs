namespace LearningCenter.Web.Controllers
{
    using LearningCenter.Services.Data;
    using LearningCenter.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly ICoursesService coursesService;

        public CategoriesController(ICategoriesService categoriesService, ICoursesService coursesService)
        {
            this.categoriesService = categoriesService;
            this.coursesService = coursesService;
        }

        public IActionResult Index()
        {
            var categoriesViewModel = new IndexViewModel();
            var categories = this.categoriesService.GetAll<CategoriesViewModel>();
            categoriesViewModel.Categories = categories;

            return this.View(categoriesViewModel);
        }

        public IActionResult ChosenCategory(int id, int page = 1)
        {
            var viewModel = this.categoriesService.GetTargetCategory<ChosenCategory>(id);
            const int itemsPerPage = 12;
            viewModel.Courses = viewModel.Courses.OrderByDescending(x => x.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            viewModel.PageNumber = page;
            viewModel.CoursesCount = this.coursesService.GetCountByCategoryId(id);
            viewModel.ItemsPerPage = itemsPerPage;
            return this.View(viewModel);
        }
    }
}
