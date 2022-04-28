using BlogPessoal.src.modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BlogPessoal.src.data
{
    public class BlogPessoalContexto : DbContext
    {
        protected readonly IConfiguration _configuration;

        public DbSet<UsuarioModelo> Usuario { get; set; }
        public DbSet<TemaModelo> Tema { get; set; }
        public DbSet<PostagemModelo> Postagens { get; set; }

        public BlogPessoalContexto(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

    }
}
