using AutoMapper;
using LearningCenter.Data.Models;
using LearningCenter.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCenter.Web.ViewModels.Account
{
    public class UserCoursesViewModel : IMapFrom<UserCourses>, IHaveCustomMappings
    {
        public PersonalCourseViewModel Course { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<UserCourses, UserCoursesViewModel>()
                .ForMember(x => x.Course, opt => opt.MapFrom(uc => uc.Course));
        }
    }
}
