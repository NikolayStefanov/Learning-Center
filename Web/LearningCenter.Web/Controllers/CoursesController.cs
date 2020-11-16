namespace LearningCenter.Web.Controllers
{
    using LearningCenter.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : Controller
    {
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CourseInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            return this.RedirectToAction(nameof(this.Create));
        }
    }
}
