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
        private readonly IAplicIdempotencia _aplicIdempotencia;

        public MovimentoController(IAplicMovimento aplicMovimento, 
                                   IAplicIdempotencia aplicIdempotencia)
        {
            _aplicMovimento = aplicMovimento;
            _aplicIdempotencia = aplicIdempotencia;
        }

        [HttpPost]
        [Route("Movimentacao")]
        public IActionResult Movimentacao([FromHeader] string chaveIdempotencia, [FromBody] InserirMovimentoDTO dto)
        {
            try
            {
                var resultadoIdempotencia = _aplicIdempotencia.VerificaIdempotencia(chaveIdempotencia);

                if (resultadoIdempotencia.Concluido)
                {
                    return Ok(new HttpRetornoSucesso("Movimento inserido com sucesso.", resultadoIdempotencia.Resultado));
                }

                var idMovimento = _aplicMovimento.InserirMovimento(chaveIdempotencia, dto);
                return Ok(new HttpRetornoSucesso("Movimento inserido com sucesso.", idMovimento));
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
