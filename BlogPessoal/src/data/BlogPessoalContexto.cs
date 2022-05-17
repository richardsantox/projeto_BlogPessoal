using BlogPessoal.src.modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlogPessoal.src.data
{
    /// <summary>
    /// <para>Resumo: Classe contexto, responsável por carregar contexto e definir DbSets</para>
    /// <para>Criado por: Richard Santos</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public class BlogPessoalContexto : DbContext
    {

        public DbSet<UsuarioModelo> Usuarios { get; set; }
        public DbSet<TemaModelo> Temas { get; set; }
        public DbSet<PostagemModelo> Postagens { get; set; }

        public BlogPessoalContexto(DbContextOptions<BlogPessoalContexto> opt) : base(opt)
        {

        }
    }
}
