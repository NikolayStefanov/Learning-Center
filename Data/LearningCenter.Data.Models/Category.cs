namespace LearningCenter.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LearningCenter.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.SubCategories = new HashSet<SubCategory>();
            this.Courses = new HashSet<Course>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
