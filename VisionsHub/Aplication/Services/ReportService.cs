using VisionsHub.Aplication.DTOs;
using VisionsHub.Aplication.DTOs.Filters;
using VisionsHub.Aplication.DTOs.Response;
using VisionsHub.Aplication.Interfaces;
using VisionsHub.Domain.Entities;
using VisionsHub.Infra.Repository;

namespace VisionsHub.Aplication.Services
{
    public class ReportService : IReportService
    {
        private readonly LoanRepository _loanRepository;

        public ReportService(LoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }
        public async Task<PagedResponse<Loan>> GetStudentsWithOverdueLoans(BaseFilter? filter)
        {
            return await _loanRepository.GetOverdueLoansAsync(filter);
        }

        public async Task<PagedResponse<ReportResponse>> GetMostBorrowedBooksReport(BaseFilter? filter)
        {
            var report = await _loanRepository.GetMostBorrowedBooksReport(filter);

            return report;
        }
    }
}
