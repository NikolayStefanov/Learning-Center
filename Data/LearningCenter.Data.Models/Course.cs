namespace LearningCenter.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LearningCenter.Data.Common.Models;
    using LearningCenter.Data.Models.Enums;

    public class Course : BaseDeletableModel<int>
    {
        public Course()
        {
            this.Lectures = new HashSet<Lecture>();
            this.Students = new HashSet<UserCourses>();
            this.Reviews = new HashSet<Review>();
            this.Ratings = new HashSet<CoursesRatings>();
        }

        [Required]
        public string Title { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [ForeignKey(nameof(Language))]
        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        [ForeignKey(nameof(SubCategory))]
        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual ICollection<CoursesRatings> Ratings { get; set; }

        public virtual ICollection<Lecture> Lectures { get; set; }

        public virtual ICollection<UserCourses> Students { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
