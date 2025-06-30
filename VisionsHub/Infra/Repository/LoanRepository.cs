using Microsoft.EntityFrameworkCore;
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
    }
}
