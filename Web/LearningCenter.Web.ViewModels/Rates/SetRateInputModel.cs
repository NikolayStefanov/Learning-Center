namespace LearningCenter.Web.ViewModels.Rates
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SetRateInputModel
    {
        [Required]
        [Range(1, 5)]
        public int Value { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
}
