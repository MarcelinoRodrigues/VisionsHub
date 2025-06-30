using VisionsHub.Domain.Common;
using VisionsHub.Domain.Enum;

namespace VisionsHub.Domain.Entities
{
    public class Student : BaseEntity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Registration { get; set; }
        public required string Course { get; set; }
        public Status Status { get; set; }
    }
}
