using VisionsHub.Aplication.DTOs.Filters;
using VisionsHub.Aplication.DTOs;
using VisionsHub.Domain.Entities;

namespace VisionsHub.Aplication.Interfaces
{
    public interface IReportService
    {
        Task<PagedResponse<Loan>> GetStudentsWithOverdueLoans(BaseFilter? filter);
    }
}
