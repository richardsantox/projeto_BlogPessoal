using BlogPessoal.src.dtos;
using BlogPessoal.src.modelo;
using BlogPessoal.src.repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogPessoal.src.controladores
{
    [ApiController]
    [Route("api/Postagens")]
    [Produces("application/json")]
    public class PostagemControlador : ControllerBase
    {
        #region Atributos

        private readonly IPostagem _repositorio;

        #endregion


        #region Construtores

        public PostagemControlador(IPostagem repositorio)
        {
            _repositorio = repositorio;
        }

        #endregion


        #region Métodos

        /// <summary>
        /// Resumo: Pegar postagem pelo Id
        /// </summary>
        /// <param name="idPostagem">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retorna a postagem</response>
        /// <response code="404">Postagem não existente</response>
        [HttpGet("id/{idPostagem}")]
        [Authorize]
        public async Task<ActionResult> PegarPostagemPeloIdAsync([FromRoute] int idPostagem)
        {
            var postagem = await _repositorio.PegarPostagemPeloIdAsync(idPostagem);

            if (postagem == null) return NotFound();

            return Ok(postagem);
        }


        /// <summary>
        /// Resumo: Pegar todas as postagens
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retornar todas as postagens</response>
        /// <response code="204">Sem conteúdo</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> PegarTodasPostagensAsync()
        {
            var lista = await _repositorio.PegarTodasPostagensAsync();

            if (lista.Count < 1) return NoContent();

            return Ok(lista);
        }


        /// <summary>
        /// <para>
        /// Resumo: Pegar postagem por título ou tema da descrição ou nome do criador</para>
        /// </summary>
        /// <param name="titulo">Título da postagem</param>
        /// <param name="descricaoTema">Descrição do tema da postagem</param>
        /// <param name="nomeCriador">Nome do criador da postagem</param>
        /// <return>ActionResult</return>
        /// <response code="200">Retornar a postagem</response>
        /// <response code="204">Sem conteúdo</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TemaModelo))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("pesquisa")]
        [Authorize]
        public async Task<ActionResult> PegarPostagensPorPesquisaAsync
        (
            [FromQuery] string titulo,
            [FromQuery] string descricaoTema,
            [FromQuery] string nomeCriador
        )
        {
            var postagens = await _repositorio.PegarPostagensPorPesquisaAsync(titulo,
            descricaoTema, nomeCriador);

            if (postagens.Count < 1) return NoContent();

            return Ok(postagens);
        }


        /// <summary>
        /// Resumo: Criar nova Postagem
        /// </summary>
        /// <param name="postagem">NovaPostagemDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     POST /api/Postagem
        ///     {
        ///        "titulo": "DotNet em 2022",
        ///        "descricao": "DotNet em 2022 é o futuro da programação",
        ///        "foto": "URLPHOTO",
        ///        "emailCriador": "richard@domain.com",
        ///        "descricaoTema": "DotNet"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Retorna postagem criada</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostagemModelo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NovaPostagemAsync([FromBody] NovaPostagemDTO postagem)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repositorio.NovaPostagemAsync(postagem);
            return Created($"api/Postagens", postagem);
        }


        /// <summary>
        /// Resumo: Atualizar Postagem
        /// </summary>
        /// <param name="postagem">AtualizarPostagemDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Postagem
        ///     {
        ///        "id": "1",
        ///        "titulo": "DotNet em 2022",
        ///        "descricao": "DotNet e seu futuro na programação",
        ///        "foto": "URLPHOTO",
        ///        "descricaoTema": "DotNet"            
        ///      }
        ///
        /// </remarks>
        /// <response code="200">Retorna postagem atualizada</response>
        /// <response code="400">Erro na requisição</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostagemModelo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> AtualizarPostagemAsync([FromBody] AtualizarPostagemDTO postagem)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repositorio.AtualizarPostagemAsync(postagem);
            return Ok(postagem);
        }


        /// <summary>
        /// Resumo: Deletar postagem pelo Id
        /// </summary>
        /// <param name="idPostagem">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Postagem deletada</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("deletar/{idPostagem}")]
        [Authorize]
        public async Task<ActionResult> DeletarPostagemAsync([FromRoute] int idPostagem)
        {
            await _repositorio.DeletarPostagemAsync(idPostagem);
            return NoContent();
        }
        #endregion
    }
}