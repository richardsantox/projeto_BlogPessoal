﻿using BlogPessoal.src.data;
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
    public class PostagemRepositorioTeste
    {
        private BlogPessoalContexto _contexto;
        private IUsuario _repositorioU;
        private ITema _repositorioT;
        private IPostagem _repositorioP;
        [TestInitialize]
        public void ConfiguracaoInicial()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContexto>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal")
            .Options;
            _contexto = new BlogPessoalContexto(opt);
            _repositorioU = new UsuarioRepositorio(_contexto);
            _repositorioT = new TemaRepositorio(_contexto);
            _repositorioP = new PostagemRepositorio(_contexto);

            _repositorioU.NovoUsuario(
                new NovoUsuarioDTO(
                "Gustavo Boaz",
                "gustavo@email.com",
                "134652", "URLDAFOTO"
                ));
                _repositorioU.NovoUsuario(
                new NovoUsuarioDTO(
                "Catarina Boaz",
                "catarina@email.com",
                "134652",
                "URLDAFOTO"
                ));

            _repositorioT.NovoTema(new NovoTemaDTO("C#"));
            _repositorioT.NovoTema(new NovoTemaDTO("Java"));
            _repositorioT.NovoTema(new NovoTemaDTO("Python"));
            _repositorioT.NovoTema(new NovoTemaDTO("JavaScript"));
        }


        [TestMethod]
        public void CriaTresPostagemNoSistemaRetornaTres()
        {
            _repositorioP.NovaPostagem(
                new NovaPostagemDTO(
                "C# é muito massa",
                "É uma linguagem muito utilizada no mundo",
                "URLDAFOTO",
                "gustavo@email.com",
                "C#"
                ));

            _repositorioP.NovaPostagem(
                new NovaPostagemDTO(
                "C# pode ser usado com Testes",
                "O teste unitário é importante para o desenvolvimento",
                "URLDAFOTO",
                "catarina@email.com",
                "C#"
                ));

            _repositorioP.NovaPostagem(
                new NovaPostagemDTO(
                "Java é muito massa",
                "Java também é muito utilizada no mundo",
                "URLDAFOTO",
                "gustavo@email.com",
                "Java"
                ));

            Assert.AreEqual(3, _repositorioP.PegarTodasPostagens().Count());
        }


        [TestMethod]
        [DataRow(2)]
        public void AtualizarPostagemRetornaPostagemAtualizada(int idPostagem)
        {
           
            _repositorioP.AtualizarPostagem(
                new AtualizarPostagemDTO(
                idPostagem,
                "JavaScript é muito massa",
                "JavaScript é muito utilizada no mundo",
                "URLDAFOTOATUALIZADA",
                "JavaScript"
                ));

            Assert.AreEqual(
            "JavaScript é muito massa",
            _repositorioP.PegarPostagemPeloId(idPostagem).Titulo
            );
            Assert.AreEqual(
            "JavaScript é muito utilizada no mundo",
            _repositorioP.PegarPostagemPeloId(idPostagem).Descricao
            );
            Assert.AreEqual(
            "URLDAFOTOATUALIZADA",
            _repositorioP.PegarPostagemPeloId(idPostagem).Foto
            );
            Assert.AreEqual(
            "JavaScript",
            _repositorioP.PegarPostagemPeloId(idPostagem).Tema.Descricao
            );
        }


        [TestMethod]
        [DataRow("massa", null, null)]
        public void PegarPostagensPorPesquisaRetodarCustomizada(
        string titulo,
        string descricaoTema,
        string nomeCriador
        )
        {
            var postagens = _repositorioP
            .PegarPostagensPorPesquisa(titulo, descricaoTema, nomeCriador);
            
            Assert.AreEqual(3, postagens.Count());
        }
    }
}
