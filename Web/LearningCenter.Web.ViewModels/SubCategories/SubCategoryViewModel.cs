namespace LearningCenter.Web.ViewModels.SubCategories
{
    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class SubCategoryViewModel : IMapFrom<SubCategory>
    {
        public int Id { get; set; }

        public string Title { get; set; }

    }
}
