namespace LearningCenter.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class SubcategoriesController : BaseController
    {
        public IActionResult ChosenSubcategory()
        {
            return this.View();
        }
    }
}
