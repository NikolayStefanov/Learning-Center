namespace LearningCenter.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Data;
    using LearningCenter.Web.CloudinaryHelper;
    using LearningCenter.Web.ViewModels.Courses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CoursesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly ILanguagesService languagesService;
        private readonly ISubcategoriesService subcategoriesService;
        private readonly Cloudinary cloudinary;
        private readonly ICoursesService coursesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(ICategoriesService categoriesService, ILanguagesService languagesService, ISubcategoriesService subcategoriesService, Cloudinary cloudinary, ICoursesService coursesService, UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.languagesService = languagesService;
            this.subcategoriesService = subcategoriesService;
            this.cloudinary = cloudinary;
            this.coursesService = coursesService;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateCourseInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllAsSelectListItems();
            viewModel.SubcategoriesItems = this.subcategoriesService.GetAllAsSelectListItems();
            viewModel.LanguagesItems = this.languagesService.GetAllAsSelectListItems();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var resultUrl = await CloudinaryExtentsion.UploadAsync(this.cloudinary, inputModel.Thumbnail);
            var url = resultUrl;
            var userId = this.userManager.GetUserId(this.User);
            var newCourseId = await this.coursesService.AddCourseAsync(inputModel, url, userId);

            return this.Redirect($"/Courses/GetCourse/{newCourseId}");
        }

        public IActionResult GetCourse(int id)
        {
            var viewModel = this.coursesService.GetCourse<CourseViewModel>(id);
            return this.View(viewModel);
        }

        public async Task<IActionResult> AddCourseInBag(int id)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.coursesService.AddCourseToBagAsync(id, userId);

            return this.Redirect($"/Account/Index/{userId}#courses");
        }

        public async Task<IActionResult> RemoveCourseFromBag(int id)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.coursesService.RemoveCourseFromBag(id, userId);

            return this.Redirect($"/Account/Index/{userId}#courses");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var isDeleted = await this.coursesService.DeleteAsync(id, userId);
            if (!isDeleted)
            {
                return this.Redirect("/Home/Error");
            }

            return this.Redirect($"/Account/Index/{userId}");
        }
    }
}
