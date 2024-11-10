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
            catch (ValidacaoDadosException ex)
            {
                var httpRetornoFalha = new HttpRetornoFalha(ex.Message, ex.Tipo);
                return BadRequest(httpRetornoFalha);
            }
            catch (Exception ex)
            {
                var httpRetornoFalha = new HttpRetornoFalha(ex.Message, "UNKNOWN_ERROR");
                return BadRequest(httpRetornoFalha);
            }
        }
    }
}
