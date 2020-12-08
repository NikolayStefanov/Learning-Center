namespace LearningCenter.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRateService
    {
        Task SetRate(string userId, int courseId, int value);

        public double GetAverageRating(int courseId);

        public int GetRatesCount(int courseId);
    }
}
