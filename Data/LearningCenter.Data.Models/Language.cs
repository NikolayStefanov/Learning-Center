namespace LearningCenter.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LearningCenter.Data.Common.Models;

    public class Language : BaseDeletableModel<int>
    {
        public Language()
        {
            this.Courses = new HashSet<Course>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
