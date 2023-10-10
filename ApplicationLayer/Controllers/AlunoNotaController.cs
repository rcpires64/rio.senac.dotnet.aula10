using DomainLayer.Interfaces.Repository;
using DomainLayer.Interfaces.Service;
using DomainLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApplicationLayer.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar o cadastro de alunos
    /// </summary>
    [ApiController]
    [Route("api/nota")]
    public class AlunoNotaController : ControllerBase
    {
        private readonly ILogger<AlunoController> _logger;
        private readonly IAlunoService _alunoService;

        /// <inheritdoc />
        public AlunoNotaController(ILogger<AlunoController> logger, IAlunoService alunoService)
        {
            _logger = logger;
            _alunoService = alunoService;
        }

        /// <summary>
        /// Método responsável por listar todas as notas de um alunos cadastrado
        /// </summary>
        /// <returns>200, 400</returns>
        [HttpGet("{matricula}")]
        [SwaggerOperation("Busca as notas de um aluno")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<ActionResult<IEnumerable<Aluno>>> BuscaNotas([FromRoute] string matricula)
        {
            var notas = await _alunoService.BuscaNotas(matricula);

            return Ok(notas);
        }

        /// <summary>
        /// Método responsável por registrar as notas de um aluno no sistema
        /// </summary>
        /// <returns>201, 400</returns>
        [HttpPost()]
        [SwaggerOperation("Registra notas de um aluno")]
        [SwaggerResponse(201)]
        [SwaggerResponse(400)]
        public async Task<ActionResult> RegistraNotas([FromBody] AlunoNotasViewModel viewModel)
        {
            await _alunoService.RegistraNotas(viewModel);

            return Created("", "");
        }

        /// <summary>
        /// Método responsável por consultar a situação de avaliação do aluno
        /// </summary>
        /// <returns>200, 400</returns>
        [HttpGet("situacao/{matricula}")]
        [SwaggerOperation("Consulta a situação do aluno")]
        [SwaggerResponse(200)]
        [SwaggerResponse(400)]
        public async Task<ActionResult> Situacao([FromRoute] string matricula)
        {
            var result = await _alunoService.SituacaoAsync(matricula);

            return Ok(result);
        }
    }
}