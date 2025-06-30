using Microsoft.EntityFrameworkCore;
using VisionsHub.Aplication.DTOs;
using VisionsHub.Aplication.DTOs.Filters;
using VisionsHub.Aplication.DTOs.Request;
using VisionsHub.Domain.Entities;
using VisionsHub.Infra.Data.Context;

namespace VisionsHub.Infra.Repository
{
    public class BookRepository
    {
        private readonly BaseContext _context;

        public BookRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<Book>> GetBookByFilter(BookFilter? requestFilter)
        {
            var query = _context.Book.AsQueryable();

            if (requestFilter?.Title != null)
                query = query.Where(x => x.Title == requestFilter.Title);

            if (requestFilter?.Author != null)
                query = query.Where(x => x.Author == requestFilter.Author);

            if (requestFilter?.ISBN != null)
                query = query.Where(x => x.ISBN == requestFilter.ISBN);

            int page = requestFilter?.Page ?? 1;
            int pageSize = requestFilter?.PageSize ?? 10;

            var totalItems = await query.CountAsync();

            var books = await query
                .OrderByDescending(x => x.AvailableQuantity)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new Book
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    ISBN = x.ISBN,
                    YearOfPublication = x.YearOfPublication,
                    Category = x.Category,
                    TotalQuantity = x.TotalQuantity,
                    AvailableQuantity = x.AvailableQuantity,
                })
                .ToListAsync();

            return new PagedResponse<Book>
            {
                Items = books,
                HasNextPage = page * pageSize < totalItems
            };
        }

        public async Task CreateAsync(BookRequest request)
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

            _context.Book.Add(book);
            await _context.SaveChangesAsync();
        }
    }
}
