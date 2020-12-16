namespace LearningCenter.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using LearningCenter.Web.ViewModels.Reviews;

    public class ReviewsService : IReviewsService
    {
        private readonly IDeletableEntityRepository<Review> reviewRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public ReviewsService(IDeletableEntityRepository<Review> reviewRepository, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.reviewRepository = reviewRepository;
            this.userRepository = userRepository;
        }

        public async Task<int> AddAsync(ReviewInputModel inputModel)
        {
            var author = this.userRepository.All().FirstOrDefault(x => x.Email == inputModel.AuthorEmail);
            var newReview = new Review { AuthorId = author.Id, CourseId = inputModel.CourseId, Content = inputModel.Content };
            await this.reviewRepository.AddAsync(newReview);
            await this.reviewRepository.SaveChangesAsync();
            return newReview.Id;
        }

        public T GetReviewById<T>(int id)
        {
            return this.reviewRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }
    }
}
