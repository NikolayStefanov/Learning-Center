namespace LearningCenter.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using LearningCenter.Web.ViewModels.Account;
    using Microsoft.EntityFrameworkCore;

    public class AccountsService : IAccountsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Feedback> feedbacksRepository;

        public AccountsService(IDeletableEntityRepository<ApplicationUser> userRepository, IDeletableEntityRepository<Feedback> feedbacksRepository)
        {
            this.userRepository = userRepository;
            this.feedbacksRepository = feedbacksRepository;
        }

        public async Task<int> AddFeedbackAsync(FeedbackInputModel inputModel)
        {
            var lecturer = this.userRepository.All().Include(x => x.Lecturer).Where(x => x.Id == inputModel.LecturerId).FirstOrDefault();
            var feedbackAuthor = this.userRepository.AllAsNoTracking().FirstOrDefault(x => x.Email == inputModel.AuthorEmail);
            var feedback = new Feedback { AuthorId = feedbackAuthor.Id, LecturerId = lecturer.Lecturer.Id, Content = inputModel.Content };

            await this.feedbacksRepository.AddAsync(feedback);
            await this.feedbacksRepository.SaveChangesAsync();
            lecturer.Lecturer.Feedbacks.Add(feedback);

            return feedback.Id;
        }

        public T GetFeedbackById<T>(int id)
        {
            var targetFeedback = this.feedbacksRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
            return targetFeedback;
        }

        public T GetLecturerById<T>(string id)
        {
            return this.userRepository.AllAsNoTracking().Where(u => u.Id == id).To<T>().FirstOrDefault();
        }
    }
}
