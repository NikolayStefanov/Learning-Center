namespace LearningCenter.Web.ViewModels.Courses
{
    using System.ComponentModel.DataAnnotations;

    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;

    public class CourseInputModel : IMapTo<Course>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Title { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SubcategoryId { get; set; }

        public CourseThumbnailInputModel Thumbnail { get; set; }
    }
}
