namespace LearningCenter.Web.Controllers
{
    using LearningCenter.Services.Data;
    using LearningCenter.Web.ViewModels.About;
    using Microsoft.AspNetCore.Mvc;

    public class AboutController : BaseController
    {
        private readonly IAboutService aboutService;

        public AboutController(IAboutService aboutService)
        {
            this.aboutService = aboutService;
        }

        public IActionResult Index()
        {
            var topLecturers = this.aboutService.GetTopLecturers<TopLectorerViewModel>();
            var siteInfo = this.aboutService.GetAboutInfo();
            var viewModel = new AboutPageViewModel { Lectorers = topLecturers, LearningCenterInfo = siteInfo };
            return this.View(viewModel);
        }
    }
}
