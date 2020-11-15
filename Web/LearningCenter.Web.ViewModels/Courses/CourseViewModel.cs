namespace LearningCenter.Web.ViewModels.Courses
{
    using System.Collections.Generic;

    using LearningCenter.Data.Models;
    using LearningCenter.Services.Mapping;
    using LearningCenter.Web.ViewModels.Lectures;

    public class CourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<LectureViewModel> Lectures { get; set; }

        public string ThumbnailUrl => $""; //ДА ДОБАВЯ МЯСТО и ЛОГИКА ОТ КЪДЕТО и КАК ЩЕ СЕ ВЗИМА СНИМКАТА НА КУРСА
    }
}
