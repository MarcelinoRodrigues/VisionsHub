using VisionsHub.Aplication.DTOs;
using VisionsHub.Aplication.DTOs.Filters;
using VisionsHub.Aplication.DTOs.Request;
using VisionsHub.Domain.Entities;

namespace VisionsHub.Aplication.Interfaces
{
    public interface IBookService
    {
        Task Create(BookRequest request);

        Task<PagedResponse<Book>> GetBooks(BookFilter? filter);
    }
}
