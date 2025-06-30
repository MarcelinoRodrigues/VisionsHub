namespace VisionsHub.Aplication.DTOs
{
    public class PagedResponse<T>
    {
        public List<T> Items { get; set; } = new();
        public bool HasNextPage { get; set; }
    }
}
