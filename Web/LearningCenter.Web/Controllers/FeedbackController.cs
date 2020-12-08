namespace LearningCenter.Web.Controllers
{
    using System.Threading.Tasks;

    using LearningCenter.Services.Data;
    using LearningCenter.Web.ViewModels.Account;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IAccountsService accountService;

        public FeedbackController(IAccountsService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult<FeedbackViewModel>> AddFeedback(FeedbackInputModel inputModel)
        {
            var newFeedbackId = await this.accountService.AddFeedbackAsync(inputModel);
            var resultObject = this.accountService.GetFeedbackById<FeedbackViewModel>(newFeedbackId);
            return resultObject;
        }
    }
}
