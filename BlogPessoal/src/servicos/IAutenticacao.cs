using BlogPessoal.src.dtos;
using BlogPessoal.src.modelo;

namespace BlogPessoal.src.servicos
{
    public interface IAutenticacao
    {
        string CodificarSenha(string senha);
        void CriarUsuarioSemDuplicar(NovoUsuarioDTO dto);
        string GerarToken(UsuarioModelo usuario);
        AutorizacaoDTO PegarAutorizacao(AutenticarDTO dto);
    }

}
