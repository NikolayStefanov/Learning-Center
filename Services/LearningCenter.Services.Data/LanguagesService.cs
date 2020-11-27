namespace LearningCenter.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class LanguagesService : ILanguagesService
    {
        private readonly IDeletableEntityRepository<Language> languageRepo;

        public LanguagesService(IDeletableEntityRepository<Language> languageRepo)
        {
            this.languageRepo = languageRepo;
        }

        public IEnumerable<SelectListItem> GetAllAsSelectListItems()
        {
            var languges = this.languageRepo
                .AllAsNoTracking()
                .Select(l => new SelectListItem()
                {
                    Value = l.Id.ToString(),
                    Text = l.Name,
                })
                .ToList();
            return languges;
        }
    }
}
