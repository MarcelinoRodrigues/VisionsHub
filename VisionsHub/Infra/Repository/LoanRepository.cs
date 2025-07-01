using Microsoft.EntityFrameworkCore;
using VisionsHub.Aplication.DTOs;
using VisionsHub.Aplication.DTOs.Filters;
using VisionsHub.Aplication.DTOs.Request;
using VisionsHub.Aplication.DTOs.Response;
using VisionsHub.Domain.Entities;
using VisionsHub.Domain.Enum;
using VisionsHub.Infra.Data.Context;

namespace VisionsHub.Infra.Repository
{
    public class LoanRepository
    {
        private readonly BaseContext _context;

        public LoanRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Loan loan)
        {
            _context.Loan.Add(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<Loan?> GetLoanById(Guid id)
        {
            var loans = await _context.Loan.FindAsync(id);

            return loans ?? null;
        }

        public async Task<Book?> GetBookById(Guid id)
        {
            var book = await _context.Book.FindAsync(id);

            return book ?? null;
        }

        public async Task<bool> ReturnLoan(Guid id)
        {
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> CountActiveLoansByStudent(Guid studentId)
        {
            return await _context.Loan
                .Where(l => l.StudentId == studentId && l.ReturnLoan == null)
                .CountAsync();
        }

        public async Task<PagedResponse<Loan>> GetActiveLoad(LoadFilter? filter)
        {
            var query = _context.Loan.AsQueryable();

            if (!string.IsNullOrEmpty(filter?.Email))
            {
                query = query.Join(_context.Student,
                                   loan => loan.StudentId,
                                   student => student.Id,
                                   (loan, student) => new { loan, student })
                             .Where(x => x.student.Email == filter.Email)
                             .Select(x => x.loan);
            }

            if (!string.IsNullOrEmpty(filter?.Registration))
            {
                query = query.Join(_context.Student,
                                   loan => loan.StudentId,
                                   student => student.Id,
                                   (loan, student) => new { loan, student })
                             .Where(x => x.student.Registration == filter.Registration)
                             .Select(x => x.loan);
            }

            int page = filter?.Page ?? 1;
            int pageSize = filter?.PageSize ?? 10;

            var totalItems = await query.CountAsync();

            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Where(s => s.Status == Statusload.active)
                .Select(x => new Loan
                {
                    Id = x.Id,
                    StudentId = x.StudentId,
                    BookId = x.BookId,
                    LoanDate = x.LoanDate,
                    ExpectedReturnLoan = x.ExpectedReturnLoan,
                    ReturnLoan = x.ReturnLoan,
                    Status = x.Status,
                })
                .ToListAsync();

            return new PagedResponse<Loan>
            {
                Items = books,
                HasNextPage = page * pageSize < totalItems
            };
        }

        public async Task<PagedResponse<Loan>> GetOverdueLoansAsync(BaseFilter? filter)
        {
            var query = _context.Loan.AsQueryable();

            query = query.Where(l => l.ExpectedReturnLoan < DateTime.Now && l.ReturnLoan == null);

            int page = filter?.Page ?? 1;
            int pageSize = filter?.PageSize ?? 10;

            var totalItems = await query.CountAsync();

            var loan = await query
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .Select(x => new Loan
               {
                   Id = x.Id,
                   StudentId = x.StudentId,
                   BookId = x.BookId,
                   LoanDate = x.LoanDate,
                   ExpectedReturnLoan = x.ExpectedReturnLoan,
                   ReturnLoan = x.ReturnLoan,
                   Status = x.Status
               })
               .ToListAsync();

            return new PagedResponse<Loan>
            {
                Items = loan,
                HasNextPage = page * pageSize < totalItems
            };
        }

        public async Task<PagedResponse<ReportResponse>> GetMostBorrowedBooksReport(BaseFilter? filter)
        {
            var loans = _context.Loan.AsQueryable();

            var grouped = loans
                .GroupBy(l => l.BookId)
                .Select(g => new
                {
                    BookId = g.Key,
                    TotalLoans = g.Count(),
                    FirstLoan = g.OrderBy(l => l.LoanDate).FirstOrDefault(),
                    LastLoan = g.OrderByDescending(l => l.LoanDate).FirstOrDefault(),
                    TopStudentId = g.GroupBy(l => l.StudentId)
                                    .OrderByDescending(grp => grp.Count())
                                    .Select(grp => grp.Key)
                                    .FirstOrDefault()
                });

            int page = filter?.Page ?? 1;
            int pageSize = filter?.PageSize ?? 10;
            var totalItems = await grouped.CountAsync();

            var items = await grouped
                .OrderByDescending(x => x.TotalLoans)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var result = items.Select(x => new ReportResponse
            {
                BookId = x.BookId,
                BookTitle = _context.Book.Where(b => b.Id == x.BookId).Select(b => b.Title).FirstOrDefault() ?? "",
                FirstStudentName = x.FirstLoan != null
                    ? _context.Student.Where(s => s.Id == x.FirstLoan.StudentId).Select(s => s.Name).FirstOrDefault() ?? ""
                    : "",
                LastStudentName = x.LastLoan != null
                    ? _context.Student.Where(s => s.Id == x.LastLoan.StudentId).Select(s => s.Name).FirstOrDefault() ?? ""
                    : "",
                TopStudentName = x.TopStudentId != Guid.Empty
                    ? _context.Student.Where(s => s.Id == x.TopStudentId).Select(s => s.Name).FirstOrDefault() ?? ""
                    : "",
                TotalLoans = x.TotalLoans
            }).ToList();

            return new PagedResponse<ReportResponse>
            {
                Items = result,
                HasNextPage = page * pageSize < totalItems
            };
        }

        public async Task<PagedResponse<Loan>> GetLoanHistoryByPeriod(ReportFilter? filter)
        {
            var query = _context.Loan.AsQueryable();

            query = query.Where(l =>
                l.LoanDate >= filter!.StartDate &&
                l.LoanDate <= filter.EndDate);

            int page = filter?.Page ?? 1;
            int pageSize = filter?.PageSize ?? 10;
            var totalItems = await query.CountAsync();

            var loans = await query
                .OrderBy(l => l.LoanDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new Loan
                {
                    Id = x.Id,
                    StudentId = x.StudentId,
                    BookId = x.BookId,
                    LoanDate = x.LoanDate,
                    ExpectedReturnLoan = x.ExpectedReturnLoan,
                    ReturnLoan = x.ReturnLoan,
                    Status = x.Status
                })
                .ToListAsync();

            return new PagedResponse<Loan>
            {
                Items = loans,
                HasNextPage = page * pageSize < totalItems
            };
        }
    }
}
