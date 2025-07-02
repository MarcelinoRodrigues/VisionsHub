using VisionsHub.Domain.Entities;
using VisionsHub.Domain.Enum;

namespace VisionsHub.Aplication.DTOs.Request
{
    public class LoanRequest 
    {
        public Guid StudentId { get; set; }
        public Guid BookId { get; set; }
        public StatusLoan Status { get; set; }
    }
}
