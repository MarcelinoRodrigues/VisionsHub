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
            var books = await _bookRepository.GetBooks();

            if (!string.IsNullOrWhiteSpace(filter?.Title))
                books = books.Where(b => b.Title.Contains(filter.Title, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrWhiteSpace(filter?.Author))
                books = books.Where(b => b.Author.Contains(filter.Author, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrWhiteSpace(filter?.ISBN))
                books = books.Where(b => b.ISBN.Contains(filter.ISBN, StringComparison.OrdinalIgnoreCase)).ToList();

            books = books.Where(b => b.AvailableQuantity > 0).ToList();

            int page = filter?.Page ?? 1;
            int pageSize = filter?.PageSize ?? 10;
            var pagedBooks = books
                .OrderByDescending(x => x.AvailableQuantity)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResponse<Book>
            {
                Items = pagedBooks,
                HasNextPage = page * pageSize < books.Count
            };
        }
    }
}
