namespace LearningCenter.Web.ViewModels.Categories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class IndexViewModel
    {
        public IEnumerable<CategoriesViewModel> Categories { get; set; }
    }
}
