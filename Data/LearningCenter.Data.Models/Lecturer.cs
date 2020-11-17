namespace LearningCenter.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LearningCenter.Data.Common.Models;

    public class Lecturer : BaseDeletableModel<string>
    {
        public Lecturer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int Rating { get; set; }

        [Required]
        [ForeignKey(nameof(ProfilePicture))]
        public int ProfilePictureId { get; set; }

        public ProfilePicture ProfilePicture { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
