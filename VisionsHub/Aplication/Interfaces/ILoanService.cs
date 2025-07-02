using VisionsHub.Aplication.DTOs;
using VisionsHub.Aplication.DTOs.Filters;
using VisionsHub.Aplication.DTOs.Request;
using VisionsHub.Domain.Entities;

namespace VisionsHub.Aplication.Interfaces
{
    public interface ILoanService
    {
        Task Create(LoanRequest request);
        Task<bool> ReturnLoan(Guid id);
        Task<PagedResponse<Loan>> GetActiveLoan(LoanFilter? filter);
    }
}
