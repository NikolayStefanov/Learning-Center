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

        [IgnoreAntiforgeryToken]
        public ActionResult<CreateCourseDropdownModel> GetSubcategories(DropdownModel inputModel)
        {
            var subcategories = this.categoriesService.GetSubcategoriesAsSelectListItems(int.Parse(inputModel.CategoryId));
            var resultObj = new CreateCourseDropdownModel { Subcategories = subcategories };
            return resultObj;
        }
    }
}
