namespace LearningCenter.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LearningCenter.Data.Common.Models;

    public class CoursesRatings : BaseDeletableModel<int>
    {
        [Required]
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        [Required]
        [ForeignKey(nameof(Rating))]
        public int RatingId { get; set; }

        public Rating Rating { get; set; }
    }
}
