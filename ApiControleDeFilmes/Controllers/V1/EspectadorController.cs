using ApiControleDeFilmes.Exceptions;
using ApiControleDeFilmes.InputModel;
using ApiControleDeFilmes.Services;
using ApiControleDeFilmes.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiControleDeFilmes.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class EspectadorController : ControllerBase
    {
        private readonly IEspectadorService _espectadorService;
        public EspectadorController(IEspectadorService espectadorService)
        {
            _espectadorService = espectadorService;
        }

        /// <summary>
        /// Buscar todos os espectador de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possivel retornar os espectadores sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual pagina está sendo consultada, mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de registros por pagina, mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de espectadores</response>
        /// <response code="204">Caso não houver espectadores cadastrados</response>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspectadorViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var espectadores = await _espectadorService.Obter(pagina, quantidade);

            if (espectadores.Count() == 0)
                return NoContent();

            return Ok(espectadores);
        }

        /// <summary>
        /// Buscar um espectador por seu Id
        /// </summary>
        /// <param name="idEspectador">Id do espectador a ser buscado</param>
        /// <response code="200">Retorna o espectador filtrado</response>
        /// <response code="204">Caso não houver espectador cadastrados</response>
        [HttpGet("{idEspectador:guid}")]
        public async Task<ActionResult<EspectadorViewModel>> Obter([FromRoute] Guid idEspectador)
        {
            var espectador = await _espectadorService.Obter(idEspectador);

            if (espectador == null)
                return NoContent();

            return Ok(espectador);
        }

        /// <summary>
        /// Cadastra um novo espectador
        /// </summary>
        /// <param name="espectadorInputModel">Dados necessarios do espectador a ser cadastrado</param>
        /// <response code="200">Espectador cadastrado</response>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EspectadorViewModel>> InserirEspectador([FromBody] EspectadorInputModel espectadorInputModel)
        {
            try
            {
                var espectador = await _espectadorService.Inserir(espectadorInputModel);
                return Ok();
            }
            catch (EspectadorJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um espectador com este nome, sobrenome e telefone.");
            }
        }

        /// <summary>
        /// Faz a alteração do espectador encontrado pelo Id
        /// </summary>
        /// <param name="idEspectador">Id do espectador a ser alterado</param>
        /// <param name="espectadorInputModel">Busca o modelo de dados a serem alterados</param>
        /// <response code="200">Atualiza o cadastro do espectador</response>
        /// <response code="204">Caso não tiver espectador cadastrado com este Id</response>
        [HttpPut("{idEspectador:guid}")]
        public async Task<ActionResult> AtualizarEspectador([FromRoute] Guid idEspectador, [FromBody] EspectadorInputModel espectadorInputModel)
        {
            try
            {
                await _espectadorService.Atualizar(idEspectador, espectadorInputModel);
                return Ok();
            }
            catch (EspectadorNaoCadastradoException ex)
            {
                return NotFound("Não existe este filme");
            }
        }

        /// <summary>
        /// Deleta o registro do espectador
        /// </summary>
        /// <param name="idEspectador">Id do espectador a ser deletado</param>
        /// <response code="200">Espectador deletado com sucesso</response>
        [HttpDelete("{idEspectador:guid}")]
        public async Task<ActionResult> ApagarEspectador([FromRoute] Guid idEspectador)
        {
            try
            {
                await _espectadorService.Remover(idEspectador);

                return Ok();
            }
            catch (EspectadorNaoCadastradoException ex)
            {
                return NotFound("Não existe este espectador");
            }
        }
    }
}

