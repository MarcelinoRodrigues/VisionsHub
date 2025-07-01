using Microsoft.AspNetCore.Mvc;
using VisionsHub.Aplication.DTOs.Filters;
using VisionsHub.Aplication.Interfaces;

namespace VisionsHub.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;
        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("most-borrowed-books")]
        public async Task<IActionResult> GetMostBorrowedBooksReport([FromQuery] BaseFilter? filter)
        {
            var result = await _service.GetMostBorrowedBooksReport(filter);
            return Ok(result);
        }

        [HttpGet("students-with-overdue-loans")]
        public async Task<IActionResult> GetStudentsWithOverdueLoans([FromQuery] BaseFilter? filter)
        {
            var result = await _service.GetStudentsWithOverdueLoans(filter);
            return Ok(result);
        }

        [HttpGet("loan-history")]
        public async Task<IActionResult> GetLoanHistoryByPeriod([FromQuery] ReportFilter filter)
        {
            var result = await _service.GetLoanHistoryByPeriod(filter);
            return Ok(result);
        }
    }
}
