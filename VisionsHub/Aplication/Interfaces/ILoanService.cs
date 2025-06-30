using VisionsHub.Aplication.DTOs.Request;

namespace VisionsHub.Aplication.Interfaces
{
    public interface ILoanService
    {
        Task Create(LoanRequest request);
        Task<bool> ReturnLoan(Guid id);
    }
}
