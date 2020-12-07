using LearningCenter.Data.Models;
using LearningCenter.Services.Mapping;
using LearningCenter.Web.ViewModels.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCenter.Web.ViewModels.Account
{
    public class UserCoursesViewModel : IMapFrom<UserCourses>
    {
        public int CourseId { get; set; }

        public PersonalCourseViewModel Course { get; set; }

        public int? Grade { get; set; }
    }
}
