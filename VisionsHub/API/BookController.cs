using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VisionsHub.Aplication.DTOs.Filters;
using VisionsHub.Aplication.DTOs.Request;
using VisionsHub.Aplication.Interfaces;

namespace VisionsHub.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;
        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetBook([FromQuery] BookFilter? request)
        {
            var books = await _service.GetBooks(request);
            return Ok(books);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateBook([FromBody] BookRequest request)
        {
            await _service.Create(request);
            return Ok(new { mensagem = "Produto cadastrado com sucesso!" });
        }
    }
}
