namespace LearningCenter.Web.Controllers
{
    using LearningCenter.Services.Data;
    using LearningCenter.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ILanguagesService languagesService;
        private readonly ISubcategoriesService subcategoriesService;

        public CoursesController(ICategoriesService categoriesService, ILanguagesService languagesService, ISubcategoriesService subcategoriesService)
        {
            this.categoriesService = categoriesService;
            this.languagesService = languagesService;
            this.subcategoriesService = subcategoriesService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateCourseInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllAsSelectListItems();
            viewModel.SubcategoriesItems = this.subcategoriesService.GetAllAsSelectListItems();
            viewModel.LanguagesItems = this.languagesService.GetAllAsSelectListItems();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateCourseInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            return this.RedirectToAction(nameof(this.Create));
        }
    }
}
