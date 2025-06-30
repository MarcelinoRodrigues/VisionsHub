using VisionsHub.Domain.Common;
using VisionsHub.Domain.Enum;

namespace VisionsHub.Domain.Entities
{
    public class Loan : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Guid BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ExpectedReturnLoan { get; set; }
        public DateTime? ReturnLoan { get; set; }
        public Statusload Status { get; set; }
    }
}
