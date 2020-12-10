namespace LearningCenter.Web.Controllers
{
    using System.Threading.Tasks;

    using LearningCenter.Services.Data;
    using LearningCenter.Web.ViewModels.Reviews;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsService reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        [HttpPost]
        public async Task<ActionResult<ReviewViewModel>> AddFeedback(ReviewInputModel inputModel)
        {
            var newReviewId = await this.reviewsService.AddAsync(inputModel);
            var resultObject = this.reviewsService.GetReviewById<ReviewViewModel>(newReviewId);
            return resultObject;
        }
    }
}
