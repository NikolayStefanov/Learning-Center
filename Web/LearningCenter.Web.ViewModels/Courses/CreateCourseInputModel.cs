namespace LearningCenter.Web.ViewModels.Courses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateCourseInputModel : IMapTo<Course>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Title { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Subcategory")]
        public int SubcategoryId { get; set; }

        [Required]
        [Display(Name ="Upload Thumbnail")]
        public IFormFile Thumbnail { get; set; }

        public IEnumerable<SelectListItem> LanguagesItems { get; set; }

        public IEnumerable<SelectListItem> CategoriesItems { get; set; }

        public IEnumerable<SelectListItem> SubcategoriesItems { get; set; }
    }
}
