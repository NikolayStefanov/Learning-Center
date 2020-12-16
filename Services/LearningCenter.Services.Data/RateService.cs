namespace LearningCenter.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;

    public class RateService : IRateService
    {
        private readonly IRepository<Rating> rateRepository;
        private readonly IDeletableEntityRepository<Course> coursesRepository;

        public RateService(IRepository<Rating> rateRepository, IDeletableEntityRepository<Course> coursesRepository)
        {
            this.rateRepository = rateRepository;
            this.coursesRepository = coursesRepository;
        }

        public double GetAverageRating(int courseId)
        {
            var averageRating = this.coursesRepository.All().Where(x => x.Id == courseId).Select(x => x.Ratings.Average(r => r.Value)).FirstOrDefault();
            return averageRating;
        }

        public int? GetRateByUserAndCourse(int courseId, string userId)
        {
            var targetRate = this.rateRepository.All().FirstOrDefault(x => x.CourseId == courseId && x.UserId == userId);
            if (targetRate == null)
            {
                return null;
            }

            return targetRate.Value;
        }

        public int GetRatesCount(int courseId)
        {
            var ratingsCount = this.rateRepository.All().Where(x => x.CourseId == courseId).Count();
            return ratingsCount;
        }

        public async Task SetRateAsync(string userId, int courseId, int value)
        {
            var rate = this.rateRepository.All().Where(x => x.CourseId == courseId && x.UserId == userId).FirstOrDefault();

            if (rate == null)
            {
                rate = new Rating { CourseId = courseId, UserId = userId};

                await this.rateRepository.AddAsync(rate);
            }

            rate.Value = value;
            await this.rateRepository.SaveChangesAsync();
        }
    }
}
