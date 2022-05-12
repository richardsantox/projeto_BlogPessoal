using BlogPessoal.src.dtos;
using BlogPessoal.src.repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogPessoal.src.controladores
{

    [ApiController]
    [Route("api/Temas")]
    [Produces("application/json")]
    public class TemaControlador : ControllerBase
    {

        #region Atributos

        private readonly ITema _repositorio;

        #endregion


        #region Construtores

        public TemaControlador(ITema repositorio)
        {
            _repositorio = repositorio;
        }
        
        #endregion


        #region Métodos

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> PegarTodosTemasAsync()
        {
            var lista = await _repositorio.PegarTodosTemasAsync();

            if(lista.Count < 1) return NoContent();

            return Ok(lista);
        }


        [HttpGet("id/idtema")]
        [Authorize]
        public async Task<ActionResult> PegarTemaPeloIdAsync([FromRoute] int idtema)
        {
            var tema = await _repositorio.PegarTemaPeloIdAsync(idtema);

            if(tema == null) return NotFound();

            return Ok(tema);
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult> PegarTemaPelaDescricaoAsync([FromQuery] string descricao)
        {
            var tema = await _repositorio.PegarTemaPelaDescricaoAsync(descricao);

            if(tema.Count < 1) return NoContent();

            return Ok(tema);
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NovoTemaAsync([FromBody] NovoTemaDTO tema)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repositorio.NovoTemaAsync(tema);
            return Created("api/Temas", tema);
        }


        [HttpPut]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> AtualizarTemaAsync([FromBody] AtualizarTemaDTO tema)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repositorio.AtualizarTemaAsync(tema);
            return Ok(tema);
        }


        [HttpDelete("deletar/{idTema}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public async Task<ActionResult> DeletarTemaAsync([FromRoute] int idTema)
        {
            await _repositorio.DeletarTemaAsync(idTema);
            return NoContent();
        }
        #endregion
    }
}
