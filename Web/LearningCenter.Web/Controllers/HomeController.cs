namespace LearningCenter.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Data;
    using LearningCenter.Web.ViewModels;
    using LearningCenter.Web.ViewModels.Categories;
    using LearningCenter.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ISearchService searchService;
        private readonly ICoursesService coursesService;

        public HomeController(SignInManager<ApplicationUser> signInManager, ISearchService searchService, ICoursesService coursesService)
        {
            this.signInManager = signInManager;
            this.searchService = searchService;
            this.coursesService = coursesService;
        }

        public IActionResult Index()
        {
            if (this.signInManager.IsSignedIn(this.User))
            {
                return this.Redirect("/Categories");
            }

            return this.View();
        }

        public async Task<IActionResult> SearchAsync(string searchTerm, int page = 1)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                this.ModelState.AddModelError("search", "Search field is empty!");
                return this.RedirectToAction(nameof(this.Index));
            }

            var viewModel = new SearchViewModel { Courses = await this.searchService.GetSearchedCoursesAsync<CourseViewModel>(searchTerm) };
            const int itemsPerPage = 12;
            viewModel.Courses = viewModel.Courses.OrderByDescending(x => x.Id).Skip((page - 1) * itemsPerPage).Take(itemsPerPage);
            viewModel.PageNumber = page;
            viewModel.CoursesCount = viewModel.Courses.Count();
            viewModel.ItemsPerPage = itemsPerPage;
            return this.View("SearchResult", viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult StatusCodeError(int errorCode)
        {
            this.ViewData["statusCode"] = errorCode;
            this.ViewData["requestId"] = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier;
            return this.View();
        }
    }
}
