using Azure.Core;
using Microsoft.EntityFrameworkCore;
using VisionsHub.Aplication.DTOs;
using VisionsHub.Aplication.DTOs.Filters;
using VisionsHub.Aplication.DTOs.Request;
using VisionsHub.Aplication.Interfaces;
using VisionsHub.Domain.Entities;
using VisionsHub.Domain.Enum;
using VisionsHub.Infra.Repository;

namespace VisionsHub.Aplication.Services
{
    public class LoanService : ILoanService
    {
        private readonly LoanRepository _loanRepository;
        private readonly BookRepository _bookRepository;
        private readonly StudentRepository _studentRepository;

        public LoanService(LoanRepository loanRepository, BookRepository bookRepository, StudentRepository studentRepository)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _studentRepository = studentRepository;
        }

        public async Task<PagedResponse<Loan>> GetActiveLoan(LoanFilter? filter)
        {
            if (filter?.Email == null && filter?.Registration == null)
                throw new Exception("informe pelo menos um dos campos do aluno");

            var loans = await _loanRepository.GetActiveLoans();

            if (!string.IsNullOrEmpty(filter.Email))
            {
                loans = loans.Where(l =>
                    _studentRepository.GetByIdAsync(l.StudentId).Result?.Email == filter.Email
                ).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Registration))
            {
                loans = loans.Where(l =>
                    _studentRepository.GetByIdAsync(l.StudentId).Result?.Registration == filter.Registration
                ).ToList();
            }

            int page = filter.Page;
            int pageSize = filter.PageSize;
            var paged = loans
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResponse<Loan>
            {
                Items = paged,
                HasNextPage = page * pageSize < loans.Count
            };
        }

        public async Task Create(LoanRequest request)
        {
            try
            {
                var book = await _bookRepository.GetBookById(request.BookId);
                var student = await _studentRepository.GetByIdAsync(request.StudentId);
                var activeLoans = await _loanRepository.CountActiveLoansByStudent(request.StudentId);
                var overdueLoans = await _loanRepository.GetOverdueLoansAsync(new BaseFilter { Page = 1, PageSize = 1 });
                bool hasOverdue = overdueLoans.Items.Any(l => l.StudentId == request.StudentId);

                if (hasOverdue)
                    throw new Exception("Aluno com empréstimos em atraso não pode pegar novos livros.");

                if (student == null || student.Status == Status.inactive)
                    throw new Exception("Aluno inativo não pode fazer empréstimos.");

                if (activeLoans >= 3)
                    throw new Exception("Aluno já possui 3 empréstimos ativos.");

                if (book == null || book.AvailableQuantity == 0)
                    throw new Exception("sem exemplares disponíveis.");

                book.AvailableQuantity -= 1;
                await _bookRepository.UpdateAsync(book);

                var loan = new Loan
                {
                    Id = Guid.NewGuid(),
                    StudentId = request.StudentId,
                    BookId = request.BookId,
                    LoanDate = DateTime.Now,
                    ExpectedReturnLoan = DateTime.Now.AddDays(14),
                    ReturnLoan = null,
                    Status = request.Status,
                };

                await _loanRepository.CreateAsync(loan);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ReturnLoan(Guid id)
        {
            try
            {
                var loan = await _loanRepository.GetLoanById(id);

                if (loan == null || loan.Status != StatusLoan.active)
                    return false;

                loan.ReturnLoan = DateTime.Now;
                loan.Status = StatusLoan.overdue;

                var book = await _loanRepository.GetBookById(loan.BookId);

                if (book != null)
                    book.AvailableQuantity += 1;

                return await _loanRepository.ReturnLoan(id);    
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
