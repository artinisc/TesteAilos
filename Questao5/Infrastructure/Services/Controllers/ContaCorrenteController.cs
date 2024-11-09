using Microsoft.AspNetCore.Mvc;
using Questao5.Application;
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
        [Route("Listar")]
        public IActionResult Listar()
        {
            try
            {
                var contasCorrentes = _aplicContaCorrente.Listar();
                return Ok(contasCorrentes);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao listar contas correntes.");
            }
        }

        [HttpGet]
        [Route("ConsultaSaldo")]
        public IActionResult ConsultaSaldo()
        {
            try
            {
                return Ok("Quem ta rodando a cidade vai triunfar");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
