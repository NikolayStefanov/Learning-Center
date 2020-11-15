namespace LearningCenter.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class SubcategoriesController : Controller
    {
        public IActionResult ChosenSubcategory()
        {
            return this.View();
        }
    }
}
