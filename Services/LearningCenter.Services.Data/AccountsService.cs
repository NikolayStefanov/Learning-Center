namespace LearningCenter.Services.Data
{
    using System.Linq;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class AccountsService : IAccountsService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public AccountsService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public T GetLecturerById<T>(string id)
        {
            return this.userRepository.AllAsNoTracking().Where(u => u.Id == id).To<T>().FirstOrDefault();
        }
    }
}
