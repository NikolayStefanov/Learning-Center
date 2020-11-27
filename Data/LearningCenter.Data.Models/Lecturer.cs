namespace LearningCenter.Data.Models
{
    using System;
    using System.Collections.Generic;

    using LearningCenter.Data.Common.Models;

    public class Lecturer : BaseDeletableModel<string>
    {
        public Lecturer()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Feedbacks = new HashSet<Feedback>();
        }

        public virtual ICollection<Feedback> Feedbacks { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
