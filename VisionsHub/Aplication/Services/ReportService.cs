using VisionsHub.Aplication.DTOs;
using VisionsHub.Aplication.DTOs.Filters;
using VisionsHub.Aplication.DTOs.Response;
using VisionsHub.Aplication.Interfaces;
using VisionsHub.Domain.Entities;
using VisionsHub.Infra.Repository;
using static System.Reflection.Metadata.BlobBuilder;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                var loans = await _loanRepository.GetLoans();

                var filteredLoans = loans
                  .Where(l => l.LoanDate >= filter.StartDate && l.LoanDate <= filter.EndDate)
                  .ToList();

                int page = filter?.Page ?? 1;
                int pageSize = filter?.PageSize ?? 10;
                var pagedBooks = filteredLoans
                    .OrderBy(l => l.LoanDate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return new PagedResponse<Loan>
                {
                    Items = pagedBooks,
                    HasNextPage = page * pageSize < loans.Count
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
