namespace LearningCenter.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Data;
    using LearningCenter.Web.CloudinaryHelper;
    using LearningCenter.Web.ViewModels.Lectures;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Lecturer")]
    public class LecturesController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILecturesService lectureService;
        private readonly Cloudinary cloudinary;

        public LecturesController(UserManager<ApplicationUser> userManager, ILecturesService lectureService, Cloudinary cloudinary)
        {
            this.userManager = userManager;
            this.lectureService = lectureService;
            this.cloudinary = cloudinary;
        }

        public IActionResult Add(int id)
        {
            var viewModel = new LectureInputModel() { CourseId = id };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(LectureInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var userId = this.userManager.GetUserId(this.User);
            var additionalMaterialUrls = new List<string>();
            if (inputModel.AdditionalMaterials != null)
            {
                additionalMaterialUrls = await CloudinaryExtentsion.UploadManyFilesAsync(this.cloudinary, inputModel.AdditionalMaterials);
            }

            var thumbnailUrl = await CloudinaryExtentsion.UploadOneFileAsync(this.cloudinary, inputModel.Thumbnail);
            await this.lectureService.AddAsync(inputModel, userId, thumbnailUrl, additionalMaterialUrls);
            return this.Redirect($"/Courses/GetCourse/{inputModel.CourseId}");
        }
    }
}
