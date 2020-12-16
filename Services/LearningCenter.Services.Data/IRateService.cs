namespace LearningCenter.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRateService
    {
        Task SetRateAsync(string userId, int courseId, int value);

        double GetAverageRating(int courseId);

        int GetRatesCount(int courseId);

        int? GetRateByUserAndCourse(int courseId, string userId);
    }
}
