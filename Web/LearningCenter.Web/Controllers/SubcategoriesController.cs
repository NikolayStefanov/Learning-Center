namespace LearningCenter.Web.Controllers
{
    using LearningCenter.Services.Data;
    using LearningCenter.Web.ViewModels.SubCategories;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class SubcategoriesController : Controller
    {
        private readonly ISubcategoriesService subcategoriesService;
        private readonly ICoursesService coursesService;

        public SubcategoriesController(ISubcategoriesService subcategoriesService, ICoursesService coursesService)
        {
            this.subcategoriesService = subcategoriesService;
            this.coursesService = coursesService;
        }

        public IActionResult GetSubcategory(int id, int page = 1)
        {
            var viewModel = this.subcategoriesService.GetSubcategory<SubCategoryViewModel>(id);
            const int itemsPerPage = 12;
            viewModel.Courses = viewModel.Courses.OrderByDescending(x => x.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            viewModel.PageNumber = page;
            viewModel.CoursesCount = this.coursesService.GetCountBySubCategoryId(id);
            viewModel.ItemsPerPage = itemsPerPage;
            return this.View(viewModel);
        }
    }
}
