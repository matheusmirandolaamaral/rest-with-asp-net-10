namespace RestWithAspNet10.Model
{
    public class PagedSearch<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SortDirection { get; set; } = "asc";
        public int TotalResults { get; set; }
        public List<T> List { get; set; }
    }
}
