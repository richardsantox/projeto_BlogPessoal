using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositorios;
using BlogPessoal.src.repositorios.implementacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPessoalTeste.Testes.repositorios
{
    [TestClass]
    public class usuarioRepositorioTeste
    {
        private BlogPessoalContexto _contexto;
        private IUsuario _repositorio;
        [TestInitialize]
        public void ConfiguracaoInicial()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal")
            .Options;
            _contexto = new BlogPessoalContexto(opt);
            _repositorio = new UsuarioRepositorio(_contexto);
        }

        [TestMethod]
        public void CriarQuatroUsuariosNoBancoRetornaQuatroUsuarios()
        {
            //GIVEN - Dado que registro 4 usuarios no banco
            _repositorio.NovoUsuario(
            new NovoUsuarioDTO(
            "Richard",
            "richard@email.com",
            "134652",
            "URLFOTO"));

            _repositorio.NovoUsuario(
            new NovoUsuarioDTO(
            "Santos",
            "santos@email.com",
            "134652",
            "URLFOTO"));

            _repositorio.NovoUsuario(
            new NovoUsuarioDTO(
            "Pereira",
            "pereira@email.com",
            "134652",
            "URLFOTO"));

            _repositorio.NovoUsuario(
            new NovoUsuarioDTO(
            "Priscila",
            "priscila@email.com",
            "134652",
            "URLFOTO"));

            //WHEN - Quando pesquiso lista total
            //THEN - Então recebo 4 usuarios
            Assert.AreEqual(4, _contexto.Usuarios.Count());
        }

        [TestMethod]
        public void PegarUsuarioPeloEmailRetornaNaoNulo()
        {
            //GIVEN - Dado que registro um usuario no banco
            _repositorio.NovoUsuario(
            new NovoUsuarioDTO(
            "Souza",
            "souza@email.com",
            "134652",
            "URLFOTO"));

            //WHEN - Quando pesquiso pelo email deste usuario
            var user = _repositorio.PegarUsuarioPeloEmail("souza@email.com");
            
            //THEN - Então obtenho um usuario
            Assert.IsNotNull(user);
        }

        [TestMethod] 
        public void PegarUsuarioPeloIdRetornaNaoNuloENomeDoUsuario()
        {
            //GIVEN - Dado que registro um usuario no banco
            _repositorio.NovoUsuario(
            new NovoUsuarioDTO(
            "Neusa Boaz",
            "neusa@email.com",
            "134652",
            "URLFOTO"));

            //WHEN - Quando pesquiso pelo id 6
            var user = _repositorio.PegarUsuarioPeloId(7);
           
            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(user);
            
            //THEN - Então, o elemento deve ser Neusa Boaz
            Assert.AreEqual("Neusa Boaz", user.Nome);
        }

        [TestMethod]
        public void AtualizarUsuarioRetornaUsuarioAtualizado()
        {
            //GIVEN - Dado que registro um usuario no banco
            _repositorio.NovoUsuario(
            new NovoUsuarioDTO(
            "Estefânia Boaz",
            "estefania@email.com",
            "134652",
            "URLFOTO"));

            //WHEN - Quando atualizamos o usuario
            var antigo =
            _repositorio.PegarUsuarioPeloEmail("estefania@email.com");
            _repositorio.AtualizarUsuario(
            new AtualizarUsuarioDTO(
            5,
            "Estefânia Moura",
            "123456",
            "URLFOTONOVA"
            ));

            //THEN - Então, quando validamos pesquisa deve retornar nome Estefânia Moura
            Assert.AreEqual(
            "Estefânia Moura",
            _contexto.Usuarios.FirstOrDefault(u => u.Id == antigo.Id).Nome);
            
            //THEN - Então, quando validamos pesquisa deve retornar senha 123456
            Assert.AreEqual(
            "123456",
            _contexto.Usuarios.FirstOrDefault(u => u.Id ==
            antigo.Id).Senha);
        }

    }
}
