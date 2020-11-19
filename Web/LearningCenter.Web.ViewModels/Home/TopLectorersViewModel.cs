using LearningCenter.Data.Models;
using LearningCenter.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningCenter.Web.ViewModels.Home
{
    public class TopLectorersViewModel: IMapFrom<ApplicationUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public string  { get; set; }
    }
}
