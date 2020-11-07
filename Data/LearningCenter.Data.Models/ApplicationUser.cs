// ReSharper disable VirtualMemberCallInConstructor
namespace LearningCenter.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LearningCenter.Data.Common.Models;
    using LearningCenter.Data.Models.Enums;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Courses = new HashSet<UserCourses>();
        }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public GenderEnum Gender { get; set; }

        [Required]
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public Country Country { get; set; }

        // Users role addition
        [ForeignKey(nameof(Student))]
        public string StudentId { get; set; }

        public Student Student { get; set; }

        [ForeignKey(nameof(Lecturer))]
        public string LecturerId { get; set; }

        public Lecturer Lecturer { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<UserCourses> Courses { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
