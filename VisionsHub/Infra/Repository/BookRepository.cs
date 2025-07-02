using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Book>> GetBooks()
        {
            return await _context.Book.ToListAsync();
        }

        public async Task<Book?> GetBookById(Guid id)
        {
            return await _context.Book.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Book book)
        {
            _context.Book.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Book.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
