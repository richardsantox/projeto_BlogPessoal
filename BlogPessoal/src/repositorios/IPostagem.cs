﻿using BlogPessoal.src.dtos;
using BlogPessoal.src.modelo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositorios
{
    /// <summary>
    /// <para>Resumo: Responsávelpor representar ações de CRUD de Postagem</para>
    /// <para>Crriado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public interface IPostagem
    {
        Task NovaPostagemAsync(NovaPostagemDTO postagem);
        Task AtualizarPostagemAsync(AtualizarPostagemDTO postagem);
        Task DeletarPostagemAsync(int id);
        Task<PostagemModelo> PegarPostagemPeloIdAsync(int id);
        Task<List<PostagemModelo>> PegarTodasPostagensAsync(); 
        Task<List<PostagemModelo>> PegarPostagensPorPesquisaAsync(string titulo, string descricaoTema, string nomeCriador);

    }
}
