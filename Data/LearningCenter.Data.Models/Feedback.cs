namespace LearningCenter.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LearningCenter.Data.Common.Models;

    public class Feedback : BaseDeletableModel<int>
    {
        [Required]
        [ForeignKey(nameof(Lecturer))]
        public string LecturerId { get; set; }

        public virtual Lecturer Lecturer { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
