﻿using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Resumo: Classe espelho para criar uma nova Postagem</para>
    /// <para>Crriado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class NovaPostagemDTO
    {
        [Required, StringLength(30)]
        public string Titulo { get; set; }

        [Required, StringLength(100)]
        public string Descricao { get; set; }

        public string Foto { get; set; }

        [Required]
        public string EmailCriador { get; set; }

        [Required]
        public string DescricaoTema { get; set; }

        public NovaPostagemDTO(string titulo, string descricao, string foto, string emailCriador, string descricaoTema)
        {
            Titulo = titulo;
            Descricao = descricao;
            Foto = foto;
            EmailCriador = emailCriador;
            DescricaoTema = descricaoTema;
        }
    }

    /// <summary>
    /// <para>Resumo: Classe espelho para atualizar uma novo Postagem</para>
    /// <para>Crriado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class AtualizarPostagemDTO
    {
        [Required]
        public int Id { get; }

        [Required, StringLength(30)]
        public string Titulo { get; set; }

        [Required, StringLength(100)]
        public string Descricao { get; set; }

        public string Foto { get; set; }

        [Required]
        public string DescricaoTema { get; set; }

        public AtualizarPostagemDTO(int id, string titulo, string descricao, string foto, string descricaoTema)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Foto = foto;
            DescricaoTema = descricaoTema;
        }
    }
}
