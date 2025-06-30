using Microsoft.AspNetCore.Mvc;
using VisionsHub.Aplication.DTOs.Request;
using VisionsHub.Aplication.Interfaces;

namespace VisionsHub.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpPost("CreateStudent")]
        public async Task<IActionResult> Create([FromBody] StudentRequest request)
        {
            await _service.Create(request);
            return Ok(new { mensagem = "Aluno criado com sucesso!" });
        }
    }
}
