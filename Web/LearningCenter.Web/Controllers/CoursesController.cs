namespace LearningCenter.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : Controller
    {
        public IActionResult SubCategories(int id)
        {
            return this.View();
        }
    }
}
