using Microsoft.EntityFrameworkCore;
using VisionsHub.Aplication.DTOs;
using VisionsHub.Aplication.DTOs.Filters;
using VisionsHub.Aplication.DTOs.Request;
using VisionsHub.Aplication.Interfaces;
using VisionsHub.Domain.Entities;
using VisionsHub.Infra.Repository;

namespace VisionsHub.Aplication.Services
{
    public class BookService : IBookService
    {
        private readonly BookRepository _bookRepository;

        public BookService(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }   
        public async Task Create(BookRequest request)
        {
            try
            {
                await _bookRepository.CreateAsync(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PagedResponse<Book>> GetBooks(BookFilter? filter)
        {
            try
            {
                return await _bookRepository.GetBookByFilter(filter);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
