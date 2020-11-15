namespace LearningCenter.Data.Models
{
    using System.Collections.Generic;

    using LearningCenter.Data.Common.Models;

    public class Rating : BaseDeletableModel<int>
    {
        public Rating()
        {
            this.Courses = new HashSet<CoursesRatings>();
        }

        public int Value { get; set; }

        public virtual ICollection<CoursesRatings> Courses { get; set; }
    }
}
