using LearningCenter.Data.Models;
using LearningCenter.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCenter.Web.ViewModels.Account
{
    public class UserCoursesViewModel : IMapFrom<UserCourses>
    {
        public CourseViewModel Course { get; set; }
    }
}
