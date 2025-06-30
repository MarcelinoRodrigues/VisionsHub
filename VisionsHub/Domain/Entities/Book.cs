using VisionsHub.Domain.Common;

namespace VisionsHub.Domain.Entities
{
    public class Book : BaseEntity
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public int YearOfPublication { get; set; }
        public required string Category { get; set; }
        public int TotalQuantity { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
