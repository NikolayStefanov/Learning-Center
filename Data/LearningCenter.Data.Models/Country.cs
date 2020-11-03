namespace LearningCenter.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LearningCenter.Data.Common.Models;

    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            this.Users = new HashSet<ApplicationUser>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
