namespace LearningCenter.Web.Controllers
{
    using LearningCenter.Services.Data;
    using LearningCenter.Web.ViewModels.Account;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

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
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<FeedbackViewModel>> AddFeedback(FeedbackInputModel inputModel)
        {
            var newFeedbackId = await this.accountService.AddFeedbackAsync(inputModel);
            var resultObject = this.accountService.GetFeedbackById<FeedbackViewModel>(newFeedbackId);
            return resultObject;
        }
    }
}
