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
            try
            {
                return await _loanRepository.GetOverdueLoansAsync(filter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PagedResponse<ReportResponse>> GetMostBorrowedBooksReport(BaseFilter? filter)
        {
            try
            {
                var report = await _loanRepository.GetMostBorrowedBooksReport(filter);

                return report;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PagedResponse<Loan>> GetLoanHistoryByPeriod(ReportFilter filter)
        {
            try
            {
                return await _loanRepository.GetLoanHistoryByPeriod(filter);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
