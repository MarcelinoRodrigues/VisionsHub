using Microsoft.AspNetCore.Mvc;
using VisionsHub.Aplication.DTOs.Request;
using VisionsHub.Aplication.Interfaces;

namespace VisionsHub.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _service;
        public LoanController(ILoanService service)
        {
            _service = service;
        }

        [HttpPost("CreateLoan")]
        public async Task<IActionResult> CreateLoan([FromBody] LoanRequest request)
        {
            await _service.Create(request);
            return Ok(new { mensagem = "Emprestimo realizado com sucesso!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ReturnLoan(Guid id)
        {
            await _service.ReturnLoan(id);

            return Ok(new { mensagem = "Emprestimo atualizado com sucesso!" });
        }
    }
}
