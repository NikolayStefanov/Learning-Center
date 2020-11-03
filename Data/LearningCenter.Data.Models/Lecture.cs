namespace LearningCenter.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LearningCenter.Data.Common.Models;

    public class Lecture : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan Duration { get; set; }

        [ForeignKey(nameof(Author))]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }
    }
}
