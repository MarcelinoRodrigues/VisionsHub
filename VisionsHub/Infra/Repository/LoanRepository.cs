using Microsoft.EntityFrameworkCore;
using VisionsHub.Aplication.DTOs;
using VisionsHub.Aplication.DTOs.Filters;
using VisionsHub.Aplication.DTOs.Request;
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

        public async Task CreateAsync(LoanRequest request)
        {
            var loan = new Loan
            {
                Id = Guid.NewGuid(),
                StudentId = request.StudentId,
                BookId = request.BookId,
                LoanDate = request.LoanDate,
                ExpectedReturnLoan = request.ExpectedReturnLoan,
                ReturnLoan = request.ReturnLoan,
                Status = request.Status,
            };

            _context.Loan.Add(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ReturnLoan(Guid id)
        {
            var loan = await _context.Loan.FindAsync(id);

            if (loan == null || loan.Status != Statusload.active)
                return false;

            loan.ReturnLoan = DateTime.Now;
            loan.Status = Statusload.overdue;

            var book = await _context.Book.FindAsync(loan.BookId);
            if (book != null)
                book.AvailableQuantity += 1;

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
    }
}
