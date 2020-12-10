namespace LearningCenter.Web.ViewModels.Reviews
{
    using System.ComponentModel.DataAnnotations;

    public class ReviewInputModel
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public string AuthorEmail { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
