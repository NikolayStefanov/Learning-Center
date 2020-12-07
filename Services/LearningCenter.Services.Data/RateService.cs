namespace LearningCenter.Services.Data
{
    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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

        public async Task SetRate(string userId, int courseId, int value)
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
