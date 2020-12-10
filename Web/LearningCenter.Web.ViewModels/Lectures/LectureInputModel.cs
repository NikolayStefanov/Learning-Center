namespace LearningCenter.Web.ViewModels.Lectures
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class LectureInputModel
    {
        [Required]
        [Display(Name = "Lecture's video")]
        public string VideoUrl { get; set; }

        [Required]
        public IFormFile Thumbnail { get; set; }

        [Required]
        [MinLength(6)]
        public string Title { get; set; }

        [Required]
        [MinLength(30)]
        public string Description { get; set; }

        public int CourseId { get; set; }

        public IEnumerable<IFormFile> AdditionalMaterials { get; set; }
    }
}
