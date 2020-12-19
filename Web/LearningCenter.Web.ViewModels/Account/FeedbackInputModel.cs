namespace LearningCenter.Web.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;

    public class FeedbackInputModel
    {
        [Required]
        public string LecturerId { get; set; }

        [Required]
        [Display(Name ="Your Email:")]
        public string AuthorEmail { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
