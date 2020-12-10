namespace LearningCenter.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using LearningCenter.Data.Common.Models;

    public class AdditionalResource : BaseDeletableModel<int>
    {
        public string ResourceUrl { get; set; }

        [ForeignKey(nameof(Lecture))]
        public int LectureId { get; set; }

        public virtual Lecture Lecture { get; set; }
    }
}
