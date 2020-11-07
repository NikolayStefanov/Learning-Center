namespace LearningCenter.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LearningCenter.Data.Common.Models;

    public class UserCourses : BaseDeletableModel<int>
    {
        [Required]
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        [Required]
        [ForeignKey(nameof(Student))]
        public string StudentId { get; set; }

        public virtual ApplicationUser Student { get; set; }

        public int? Grade { get; set; }
    }
}
