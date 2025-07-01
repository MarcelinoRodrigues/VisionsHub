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
                var book = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    Author = request.Author,
                    ISBN = request.ISBN,
                    YearOfPublication = request.YearOfPublication,
                    Category = request.Category,
                    TotalQuantity = request.TotalQuantity,
                    AvailableQuantity = request.AvailableQuantity
                };

                await _bookRepository.CreateAsync(book);
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
