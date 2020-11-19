namespace LearningCenter.Web.ViewModels.Home
{

    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class ProfilePictureViewModel : IMapFrom<ProfilePicture>
    {
        public string Url { get; set; }
    }
}
