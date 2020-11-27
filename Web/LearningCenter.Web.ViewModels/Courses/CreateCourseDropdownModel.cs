namespace LearningCenter.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateCourseDropdownModel
    {
        public IEnumerable<SelectListItem> Subcategories { get; set; }
    }
}
