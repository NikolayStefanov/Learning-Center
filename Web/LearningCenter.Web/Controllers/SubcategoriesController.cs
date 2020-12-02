namespace LearningCenter.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class SubcategoriesController : BaseController
    {
        public IActionResult ChosenSubcategory(int id)
        {
            return this.View();
        }
    }
}
