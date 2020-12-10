namespace LearningCenter.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LearningCenter.Data.Common.Repositories;
    using LearningCenter.Data.Models;
    using LearningCenter.Web.ViewModels.Lectures;

    public class LecturesService : ILecturesService
    {
        private readonly IDeletableEntityRepository<Lecture> lecturesRepository;
        private readonly IDeletableEntityRepository<AdditionalResource> resourcesRepository;

        public LecturesService(IDeletableEntityRepository<Lecture> lecturesRepository, IDeletableEntityRepository<AdditionalResource> resourcesRepository)
        {
            this.lecturesRepository = lecturesRepository;
            this.resourcesRepository = resourcesRepository;
        }

        public async Task<int> AddAsync(LectureInputModel inputModel, string userId, string thumbnailUrl, IEnumerable<string> additionalMaterials)
        {
            var lecture = new Lecture()
            {
                Title = inputModel.Title,
                Description = inputModel.Description,
                ThumbnailUrl = thumbnailUrl,
                VideoUrl = inputModel.VideoUrl,
                CourseId = inputModel.CourseId,
                AuthorId = userId,
            };

            foreach (var resource in additionalMaterials)
            {
                var newResource = new AdditionalResource { LectureId = lecture.Id, ResourceUrl = resource };
                await this.resourcesRepository.AddAsync(newResource);
                lecture.AdditionalResources.Add(newResource);
            }

            await this.lecturesRepository.AddAsync(lecture);
            await this.lecturesRepository.SaveChangesAsync();
            await this.resourcesRepository.SaveChangesAsync();
            return lecture.Id;
        }
    }
}
