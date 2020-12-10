namespace LearningCenter.Web.ViewModels.Courses
{
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class AdditionalResourceViewModel : IMapFrom<AdditionalResource>
    {
        public string ResourceUrl { get; set; }
    }
}
