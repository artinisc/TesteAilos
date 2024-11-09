using Microsoft.AspNetCore.Mvc;
using Questao5.Application;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("Movimento")]
    public class MovimentoController : ControllerBase
    {
        private readonly IAplicMovimento _aplicMovimento;

        public MovimentoController(IAplicMovimento aplicMovimento)
        {
            _aplicMovimento = aplicMovimento;
        }

        [HttpPost]
        [Route("Movimentacao")]
        public IActionResult Movimentacao([FromBody] InserirMovimentoDTO dto)
        {
            try
            {
                var idMovimento = _aplicMovimento.InserirMovimento(dto);
                return Ok(idMovimento);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao inserir movimentação: {ex.Message}");
            }
        }
    }
}
