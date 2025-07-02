using VisionsHub.Domain.Common;
using VisionsHub.Domain.Enum;

namespace VisionsHub.Domain.Entities
{
    public class Loan : BaseEntity
    {
        /// <summary>
        /// AlunoId
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// LivroId
        /// </summary>
        public Guid BookId { get; set; }
        /// <summary>
        /// Data do emprestimo
        /// </summary>
        public DateTime LoanDate { get; set; }
        /// <summary>
        /// Data prevista da devolução
        /// </summary>
        public DateTime ExpectedReturnLoan { get; set; }
        /// <summary>
        /// Data devolução Real
        /// </summary>
        public DateTime? ReturnLoan { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public StatusLoan Status { get; set; }
    }
}
