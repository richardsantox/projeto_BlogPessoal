using BlogPessoal.src.data;
using BlogPessoal.src.modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BlogPessoalTeste.Teste.data
{

    [TestClass]
    public class BlogPessoalContextoTeste
    {
        private BlogPessoalContexto _contexto;

        [TestInitialize]
        public void incio()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
                .UseInMemoryDatabase(databaseName: "bd_blogpessoalteste")
                .Options;

            _contexto = new BlogPessoalContexto(opt);
        }


        [TestMethod]
        public void InserirNovoUsuarioNoBancoRetornaUsuario()
        {
            UsuarioModelo usuario = new UsuarioModelo();

            usuario.Nome = "Richard";
            usuario.Email = "richard@email.com";
            usuario.Senha = "123456";
            usuario.Foto = "Aki é o link da foto";

            _contexto.Usuarios.Add(usuario);

            _contexto.SaveChanges();


            Assert.IsNotNull(_contexto.Usuarios.FirstOrDefault(u => u.Email == "richard@email.com"));
        }
    }
}
