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

        public AccountController(IAccountsService accountsService, UserManager<ApplicationUser> userManager)
        {
            this.accountsService = accountsService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Index(string id)
        {
            var lecturerId = id == null ? this.userManager.GetUserId(this.User) : id;
            var viewModel = this.accountsService.GetLecturerById<ProfileViewModel>(lecturerId);
            return this.View(viewModel);
        }
    }
}
