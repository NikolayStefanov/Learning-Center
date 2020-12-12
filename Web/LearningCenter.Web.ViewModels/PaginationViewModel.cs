namespace LearningCenter.Web.ViewModels
{
    using System;

    public class PaginationViewModel
    {
        public int PageNumber { get; set; }

        public int PagesCount => (int)Math.Ceiling((double)this.CoursesCount / this.ItemsPerPage);

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public int CoursesCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
