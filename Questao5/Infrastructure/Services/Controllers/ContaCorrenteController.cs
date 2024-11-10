using Microsoft.AspNetCore.Mvc;
using Questao5.Application;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("ContaCorrente")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IAplicContaCorrente _aplicContaCorrente;

        public ContaCorrenteController(IAplicContaCorrente aplicContaCorrente)
        {
            _aplicContaCorrente = aplicContaCorrente;
        }

        [HttpGet]
        [Route("ConsultaSaldo/{idConta}")]
        public IActionResult ConsultaSaldo([FromRoute] string idConta)
        {
            try
            {
                var saldoContaDTO = _aplicContaCorrente.ConsultaSaldo(idConta);

                return Ok(saldoContaDTO);
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
