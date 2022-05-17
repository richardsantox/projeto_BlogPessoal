﻿using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositorios.implementacoes
{
    /// <summary>
    /// <para>Resumo: Classe responsável por implementar ITema</para>
    /// <para>Criado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 12/05/2022</para>
    /// </summary>
    public class PostagemRepositorio : IPostagem
    {

        #region Atributos

        private readonly BlogPessoalContexto _contexto;

        #endregion Atributos


        #region Construtores

        public PostagemRepositorio(BlogPessoalContexto contexto)
        {
            _contexto = contexto;
        }

        #endregion Construtores


        #region Métodos

        /// <summary>
        /// <para>Resumo: Método assíncrono para atualizar uma postagem</para>
        /// </summary>
        /// <param name="postagem">AtualizarPostagemDTO</param>
        public async Task AtualizarPostagemAsync(AtualizarPostagemDTO postagem)
        {
            var postagemExistente = await PegarPostagemPeloIdAsync(postagem.Id);
            postagemExistente.Titulo = postagem.Titulo;
            postagemExistente.Descricao = postagem.Descricao;
            postagemExistente.Foto = postagem.Foto;
            postagemExistente.Tema = _contexto.Temas.FirstOrDefault(t => t.Descricao == postagem.DescricaoTema);

            _contexto.Postagens.Update(postagemExistente);
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para deletar uma postagem</para>
        /// </summary>
        /// <param name="id">Id da postagem</param>
        public async Task DeletarPostagemAsync(int id)
        {
            _contexto.Postagens.Remove(await PegarPostagemPeloIdAsync(id));
            await _contexto.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para salvar uma nova postagem</para>
        /// </summary>
        /// <param name="postagem">NovaPostagemDTO</param>
        public async Task NovaPostagemAsync(NovaPostagemDTO postagem)
        {
            await _contexto.Postagens.AddAsync(new PostagemModelo
            {
                Titulo = postagem.Titulo,
                Descricao = postagem.Descricao,
                Foto = postagem.Foto,
                Criador = await _contexto.Usuarios
                                    .FirstOrDefaultAsync(u => u.Email == postagem.EmailCriador),
                Tema = await _contexto.Temas
                                .FirstOrDefaultAsync(t => t.Descricao == postagem.DescricaoTema)
            });
            _contexto.SaveChanges();
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar uma postagem pelo Id</para>
        /// </summary>
        /// <param name="id">Id da postagem</param>
        /// <return>PostagemModelo</return>
        public async Task<PostagemModelo> PegarPostagemPeloIdAsync(int id)
        {
            return await _contexto.Postagens.FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para Pesquisar postagem</para>
        /// </summary>
        /// <param name="titulo">Titulo da postagem</param>
        /// <param name="descricaoTema">Descrição do tema</param>
        /// <param name="nomeCriador">Nome do criador</param>
        /// <return>Lista PostagemModelo</return>
        public async Task<List<PostagemModelo>> PegarPostagensPorPesquisaAsync(string titulo, string descricaoTema, string nomeCriador)
        {
            switch (titulo, descricaoTema, nomeCriador)
            {
                case (null, null, null):
                    return await PegarTodasPostagensAsync();

                case (null, null, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p => p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();
                case (null, _, null):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p => p.Tema.Descricao.Contains(descricaoTema))
                        .ToListAsync();
                case (_, null, null):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p => p.Titulo.Contains(titulo))
                        .ToListAsync();
                case (_, _, null):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                        p.Titulo.Contains(titulo) &
                        p.Tema.Descricao.Contains(descricaoTema))
                        .ToListAsync();
                case (null, _, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                        p.Tema.Descricao.Contains(descricaoTema) &
                        p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();
                case (_, null, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                        p.Titulo.Contains(titulo) &
                        p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();
                case (_, _, _):
                    return await _contexto.Postagens
                        .Include(p => p.Tema)
                        .Include(p => p.Criador)
                        .Where(p =>
                        p.Titulo.Contains(titulo) |
                        p.Tema.Descricao.Contains(descricaoTema) |
                        p.Criador.Nome.Contains(nomeCriador))
                        .ToListAsync();
            }
        }

        /// <summary>
        /// <para>Resumo: Método assíncrono para pegar todas as postagens</para>
        /// </summary>
        /// <return>Lista PostagemModelo</return>
        public async Task<List<PostagemModelo>> PegarTodasPostagensAsync()
        {
            return await _contexto.Postagens.ToListAsync();
        }
        #endregion Métodos
    }
}
