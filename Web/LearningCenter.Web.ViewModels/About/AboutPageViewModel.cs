namespace LearningCenter.Web.ViewModels.About
{
    using System.Collections.Generic;

    public class AboutPageViewModel
    {
        public IEnumerable<TopLectorerViewModel> Lectorers { get; set; }

        public SiteAboutViewModel LearningCenterInfo { get; set; }
    }
}
