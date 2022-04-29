using BlogPessoal.src.dtos;
using BlogPessoal.src.modelo;
using System.Collections.Generic;

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
        void NovaPostagem(NovaPostagemDTO postagem);
        void AtualizarPostagem(AtualizarPostagemDTO postagem);
        void DeletarPostagem(int id);
        PostagemModelo PegarPostagemPeloId(int id);
        List<PostagemModelo> PegarPostagens();
        List<PostagemModelo> PegarPostagensPeloTitulo(string tittulo);
        List<PostagemModelo> PegarPostagensPelaDescricao(string descricao);
    }
}
