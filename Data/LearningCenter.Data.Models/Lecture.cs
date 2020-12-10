namespace LearningCenter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LearningCenter.Data.Common.Models;

    public class Lecture : BaseDeletableModel<int>
    {
        public Lecture()
        {
            this.AdditionalResources = new HashSet<AdditionalResource>();
        }

        [Required]
        public string VideoUrl { get; set; }

        [Required]
        public string ThumbnailUrl { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Author))]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [Required]
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<AdditionalResource> AdditionalResources { get; set; }
    }
}
