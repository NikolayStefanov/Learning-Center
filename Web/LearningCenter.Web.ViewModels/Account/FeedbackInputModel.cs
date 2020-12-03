using LearningCenter.Data.Models;
using LearningCenter.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LearningCenter.Web.ViewModels.Account
{
    public class FeedbackInputModel
    {

        [Required]
        public string LecturerId { get; set; }

        [Required]
        [Display(Name ="Your Email:")]
        public string AuthorEmail { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
