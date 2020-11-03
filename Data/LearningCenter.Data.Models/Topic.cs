namespace LearningCenter.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LearningCenter.Data.Common.Models;

    public class Topic : BaseDeletableModel<int>
    {
        public Topic()
        {
            this.Courses = new HashSet<Course>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [ForeignKey(nameof(SubCategory))]
        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
