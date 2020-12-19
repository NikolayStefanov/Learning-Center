namespace LearningCenter.Web.Controllers
{
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Data;
    using LearningCenter.Web.ViewModels.Account;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class AccountController : Controller
    {
        private readonly IAccountsService accountsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICoursesService coursesService;

        public AccountController(IAccountsService accountsService, UserManager<ApplicationUser> userManager, ICoursesService coursesService)
        {
            this.accountsService = accountsService;
            this.userManager = userManager;
            this.coursesService = coursesService;
        }

        [Authorize]
        public IActionResult Index(string id, int page = 1)
        {
            const int itemsPerPage = 12;
            var lecturerId = id == null ? this.userManager.GetUserId(this.User) : id;
            var viewModel = this.accountsService.GetLecturerById<ProfileViewModel>(lecturerId);
            viewModel.PageNumber = page;
            viewModel.CoursesCount = this.coursesService.GetCountByAuthorId(id);
            viewModel.ItemsPerPage = itemsPerPage;
            return this.View(viewModel);
        }
    }
}
