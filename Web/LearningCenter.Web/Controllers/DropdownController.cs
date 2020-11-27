namespace LearningCenter.Web.Controllers
{
    using LearningCenter.Services.Data;
    using LearningCenter.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class DropdownController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;

        public DropdownController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public ActionResult<CreateCourseDropdownModel> GetSubcategories(int categoryId, string categoryValue)
        {
            var subcategories = this.categoriesService.GetSubcategoriesAsSelectListItems(categoryId, categoryValue);
            return new CreateCourseDropdownModel { Subcategories = subcategories };
        }
    }
}
