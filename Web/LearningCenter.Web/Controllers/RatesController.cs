namespace LearningCenter.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using LearningCenter.Services.Data;
    using LearningCenter.Web.ViewModels.Rates;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IRateService rateService;

        public RatesController(IRateService rateService)
        {
            this.rateService = rateService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<RateResponseModel>> Rate(SetRateInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.rateService.SetRate(userId, input.CourseId, input.Value);
            var courseAverageRating = this.rateService.GetAverageRating(input.CourseId);
            var ratesCount = this.rateService.GetRatesCount(input.CourseId);
            var resultResponseModel = new RateResponseModel { AverageRating = courseAverageRating, RatesCount = ratesCount };
            return resultResponseModel;
        }
    }
}
