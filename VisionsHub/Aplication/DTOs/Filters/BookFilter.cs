namespace VisionsHub.Aplication.DTOs.Filters
{
    public class BookFilter : BaseFilter
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
    }
}
