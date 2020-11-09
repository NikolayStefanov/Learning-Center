namespace LearningCenter.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LearningCenter.Data.Common.Models;

    public class ProfilePicture : BaseDeletableModel<int>
    {
        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]

        public string Url { get; set; }
    }
}
