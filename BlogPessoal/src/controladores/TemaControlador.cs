﻿using BlogPessoal.src.dtos;
using BlogPessoal.src.repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult PegarTodosTemas()
        {
            var lista = _repositorio.PegarTodosTemas();

            if(lista.Count < 1) return NoContent();

            return Ok(lista);
        }


        [HttpGet("id/idtema")]
        [Authorize]
        public IActionResult PegarTemaPeloId([FromRoute] int idtema)
        {
            var tema = _repositorio.PegarTemaPeloId(idtema);

            if(tema == null) return NotFound();

            return Ok(tema);
        }


        [HttpGet]
        [Authorize]
        public IActionResult PegarTemaPelaDescricao([FromQuery] string descricao)
        {
            var tema = _repositorio.PegarTemaPelaDescricao(descricao);

            if(tema.Count < 1) return NoContent();

            return Ok(tema);
        }


        [HttpPost]
        [Authorize]
        public IActionResult NovoTema([FromBody] NovoTemaDTO tema)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repositorio.NovoTema(tema);
            return Created("api/Temas", tema);
        }


        [HttpPut]
        [Authorize(Roles = "ADMINISTRADOR")]
        public IActionResult AtualizarTema([FromBody] AtualizarTemaDTO tema)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repositorio.AtualizarTema(tema);
            return Ok(tema);
        }


        [HttpDelete("deletar/{idTema}")]
        [Authorize(Roles = "ADMINISTRADOR")]
        public IActionResult DeletarTema([FromRoute] int idTema)
        {
            _repositorio.DeletarTema(idTema);
            return NoContent();
        }
        #endregion
    }
}
