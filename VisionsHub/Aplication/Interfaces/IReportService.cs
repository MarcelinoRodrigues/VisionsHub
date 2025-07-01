using VisionsHub.Aplication.DTOs.Filters;
using VisionsHub.Aplication.DTOs;
using VisionsHub.Domain.Entities;
using VisionsHub.Aplication.DTOs.Response;

namespace VisionsHub.Aplication.Interfaces
{
    public interface IReportService
    {
        Task<PagedResponse<ReportResponse>> GetMostBorrowedBooksReport(BaseFilter? filter);
        Task<PagedResponse<Loan>> GetStudentsWithOverdueLoans(BaseFilter? filter);
        //Task<PagedResponse<Loan>> GetLoanHistoryByPeriod();
    }
}
