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
    public class FilmesController : ControllerBase
    {
        private readonly IFilmeService _filmeService;
        public FilmesController(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        /// <summary>
        /// Buscar todos os filmes de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possivel retornar os filmes sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual pagina está sendo consultada, mínimo 1</param>
        /// <param name="quantidade">Indica a quantidade de registros por pagina, mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de filmes</response>
        /// <response code="204">Caso não houver filmes cadastrados</response>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmeViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var filmes = await _filmeService.Obter(pagina, quantidade);

            if (filmes.Count() == 0)
                return NoContent();

            return Ok(filmes);
        }

        /// <summary>
        /// Buscar um filme por seu Id
        /// </summary>
        /// <param name="idFilme">Id do filme a ser buscado</param>
        /// <response code="200">Retorna o filme filtrado</response>
        /// <response code="204">Caso não houver filmes cadastrados</response>
        [HttpGet("{idFilme:guid}")]
        public async Task<ActionResult<FilmeViewModel>> Obter([FromRoute] Guid idFilme)
        {
            var filme = await _filmeService.Obter(idFilme);

            if (filme == null)
                return NoContent();

            return Ok(filme);
        }

        /// <summary>
        /// Cadastra um novo filme
        /// </summary>
        /// <param name="filmeInputModel">Dados necessarios do filme a ser cadastrado</param>
        /// <response code="200">Filme cadastrado</response>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<FilmeViewModel>> InserirFilme([FromBody] FilmeInputModel filmeInputModel)
        {
            try
            {
                var filme = await _filmeService.Inserir(filmeInputModel);
                return Ok();
            }
            catch (FilmeJaCadastradoException ex)
            {
                return UnprocessableEntity("Já existe um filme com este nome para esta produtora");
            }
        }

        /// <summary>
        /// Faz a alteração do filme encontrado pelo Id
        /// </summary>
        /// <param name="idFilme">Id do filme a ser alterado</param>
        /// <param name="filmeInputModel">Busca o modelo de dados a serem alterados</param>
        /// <response code="200">Atualiza o cadastro do filme</response>
        /// <response code="204">Caso não tiver filme cadastrado com este Id</response>
        [HttpPut("{idfilme:guid}")]
        public async Task<ActionResult> AtualizarFilme([FromRoute] Guid idFilme, [FromBody] FilmeInputModel filmeInputModel)
        {
            try
            {
                await _filmeService.Atualizar(idFilme, filmeInputModel);
                return Ok();
            }
            catch (FilmeNaoCadastradoException ex)
            {
                return NotFound("Não existe este filme");
            }
        }

        /// <summary>
        /// Deleta o registro do filme
        /// </summary>
        /// <param name="idFilme">Id do filme a ser deletado</param>
        /// <response code="200">filme deletado com sucesso</response>
        [HttpDelete("{idFilme:guid}")]
        public async Task<ActionResult> ApagarFilme([FromRoute] Guid idFilme)
        {
            try
            {
                await _filmeService.Remover(idFilme);

                return Ok();
            }
            catch (FilmeNaoCadastradoException ex)
            {
                return NotFound("Não existe este filme");
            }
        }
    }
}

