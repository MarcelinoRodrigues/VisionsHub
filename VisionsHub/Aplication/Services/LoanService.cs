using Azure.Core;
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

        public async Task<PagedResponse<Loan>> GetActiveLoad(LoadFilter? filter)
        {
            if (filter?.Email == null && filter?.Registration == null)
                throw new Exception("informe pelo menos um dos campos do aluno");

            return await _loanRepository.GetActiveLoad(filter);
        }

        public async Task Create(LoanRequest request) 
        {
            var books = await _bookRepository.GetBookByFilter(null);

            var student = await _studentRepository.GetByIdAsync(request.StudentId);
            if (student == null || student.Status == Status.inactive)
                throw new Exception("Aluno inativo não pode fazer empréstimos.");

            var activeLoans = await _loanRepository.CountActiveLoansByStudent(request.StudentId);
            if (activeLoans >= 3)
                throw new Exception("Aluno já possui 3 empréstimos ativos.");

            if (books.Items.Count == 0)
            {
                throw new Exception("sem exemplares disponíveis.");
            }

            await _loanRepository.CreateAsync(request);
        }

        public async Task<bool> ReturnLoan(Guid id)
        {
            return await _loanRepository.ReturnLoan(id);
        }
    }
}
