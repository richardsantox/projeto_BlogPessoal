﻿using BlogPessoal.src.dtos;
using BlogPessoal.src.modelo;
using System.Threading.Tasks;

namespace BlogPessoal.src.servicos
{
    /// <summary>
    /// <para>Resumo: Interface Responsável por representar ações de autenticação</para>
    /// <para>Criado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public interface IAutenticacao
    {
        string CodificarSenha(string senha);
        Task CriarUsuarioSemDuplicarAsync(NovoUsuarioDTO dto);
        string GerarToken(UsuarioModelo usuario);
        Task<AutorizacaoDTO> PegarAutorizacaoAsync(AutenticarDTO dto);
    }

}
