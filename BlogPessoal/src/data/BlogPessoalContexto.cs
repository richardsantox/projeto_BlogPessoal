using BlogPessoal.src.modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlogPessoal.src.data
{
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
