using LearningCenter.Data.Models;
using LearningCenter.Services.Data;
using LearningCenter.Web.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningCenter.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountsService accountsService;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(IAccountsService accountsService, UserManager<ApplicationUser> userManager)
        {
            this.accountsService = accountsService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var lecturerId = this.userManager.GetUserId(this.User);
            var viewModel = this.accountsService.GetLecturerById<ProfileViewModel>(lecturerId);
            return this.View(viewModel);
        }
    }
}
