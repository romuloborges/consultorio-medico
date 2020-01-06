using ConsultorioMedico.Application.Service;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Usuario;
using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConsultorioMedico_Backend.Test
{
    public class TesteUnitarioAtendenteService
    {
        private readonly Mock<IAtendenteRepository> atendenteRepositoryMock;
        private readonly Mock<IUsuarioRepository> usuarioRepositoryMock;
        private readonly Mock<IEnderecoRepository> enderecoRepositoryMock;
        public TesteUnitarioAtendenteService()
        {
            this.atendenteRepositoryMock = new Mock<IAtendenteRepository>();
            this.usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            this.enderecoRepositoryMock = new Mock<IEnderecoRepository>();
        }

        [Fact]
        public void CadastrarAtendenteComEnderecoNovoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendenteCadastro = new AtendenteCadastroViewModel("Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendente = new Atendente(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", endereco.IdEndereco);
            var usuarioAtendente = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            this.atendenteRepositoryMock.SetupSequence(a => a.BuscarAtendentePorCpf(atendenteCadastro.Cpf)).Returns((Atendente) null).Returns(atendente);
            this.atendenteRepositoryMock.Setup(a => a.BuscarAtendentePorRg(atendenteCadastro.Rg)).Returns((Atendente)null);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(atendenteCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.SetupSequence(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.atendenteRepositoryMock.Setup(a => a.CadastrarAtendente(It.IsAny<Atendente>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var atendenteService = new AtendenteService(this.atendenteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.usuarioRepositoryMock.Object);

            // when
            var resultado = atendenteService.CadastrarAtendente(atendenteCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void CadastrarAtendenteComEnderecoExistenteTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendenteCadastro = new AtendenteCadastroViewModel("Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendente = new Atendente(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", endereco.IdEndereco);
            var usuarioAtendente = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            this.atendenteRepositoryMock.SetupSequence(a => a.BuscarAtendentePorCpf(atendenteCadastro.Cpf)).Returns((Atendente)null).Returns(atendente);
            this.atendenteRepositoryMock.Setup(a => a.BuscarAtendentePorRg(atendenteCadastro.Rg)).Returns((Atendente)null);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(atendenteCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.atendenteRepositoryMock.Setup(a => a.CadastrarAtendente(It.IsAny<Atendente>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var atendenteService = new AtendenteService(this.atendenteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.usuarioRepositoryMock.Object);

            // when
            var resultado = atendenteService.CadastrarAtendente(atendenteCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void CadastrarAtendenteComCpfSemMascaraTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendenteCadastro = new AtendenteCadastroViewModel("Joana", new DateTime(1980, 2, 5), "F", "12245778912", "12.345.678-1", "joana@email.com", "(34)98543-3241", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendente = new Atendente(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Joana", new DateTime(1980, 2, 5), "F", "12245778912", "12.345.678-1", "joana@email.com", "(34)98543-3241", endereco.IdEndereco);
            var usuarioAtendente = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            this.atendenteRepositoryMock.SetupSequence(a => a.BuscarAtendentePorCpf("122.457.789-12")).Returns((Atendente)null).Returns(atendente);
            this.atendenteRepositoryMock.Setup(a => a.BuscarAtendentePorRg(atendenteCadastro.Rg)).Returns((Atendente)null);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(atendenteCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.atendenteRepositoryMock.Setup(a => a.CadastrarAtendente(It.IsAny<Atendente>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var atendenteService = new AtendenteService(this.atendenteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.usuarioRepositoryMock.Object);

            // when
            var resultado = atendenteService.CadastrarAtendente(atendenteCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoCadastrarAtendenteComCpfInvalidoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendenteCadastro = new AtendenteCadastroViewModel("Joana", new DateTime(1980, 2, 5), "F", "122A45B.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendente = new Atendente(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Joana", new DateTime(1980, 2, 5), "F", "122A45B.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", endereco.IdEndereco);
            var usuarioAtendente = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            this.atendenteRepositoryMock.SetupSequence(a => a.BuscarAtendentePorCpf(atendenteCadastro.Cpf)).Returns((Atendente)null).Returns(atendente);
            this.atendenteRepositoryMock.Setup(a => a.BuscarAtendentePorRg(atendenteCadastro.Rg)).Returns((Atendente)null);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(atendenteCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.atendenteRepositoryMock.Setup(a => a.CadastrarAtendente(It.IsAny<Atendente>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var atendenteService = new AtendenteService(this.atendenteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.usuarioRepositoryMock.Object);

            // when
            var resultado = atendenteService.CadastrarAtendente(atendenteCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void CadastrarAtendenteComRgSemMascaraTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendenteCadastro = new AtendenteCadastroViewModel("Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "123456781", "joana@email.com", "(34)98543-3241", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendente = new Atendente(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "123456781", "joana@email.com", "(34)98543-3241", endereco.IdEndereco);
            var usuarioAtendente = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            this.atendenteRepositoryMock.SetupSequence(a => a.BuscarAtendentePorCpf(atendenteCadastro.Cpf)).Returns((Atendente)null).Returns(atendente);
            this.atendenteRepositoryMock.Setup(a => a.BuscarAtendentePorRg(atendenteCadastro.Rg)).Returns((Atendente)null);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(atendenteCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.atendenteRepositoryMock.Setup(a => a.CadastrarAtendente(It.IsAny<Atendente>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var atendenteService = new AtendenteService(this.atendenteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.usuarioRepositoryMock.Object);

            // when
            var resultado = atendenteService.CadastrarAtendente(atendenteCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoCadastrarAtendenteComRgInvalidoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendenteCadastro = new AtendenteCadastroViewModel("Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "A2.345.678-1", "joana@email.com", "(34)98543-3241", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendente = new Atendente(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "A2.345.678-1", "joana@email.com", "(34)98543-3241", endereco.IdEndereco);
            var usuarioAtendente = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            this.atendenteRepositoryMock.SetupSequence(a => a.BuscarAtendentePorCpf(atendenteCadastro.Cpf)).Returns((Atendente)null).Returns(atendente);
            this.atendenteRepositoryMock.Setup(a => a.BuscarAtendentePorRg(atendenteCadastro.Rg)).Returns((Atendente)null);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(atendenteCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.atendenteRepositoryMock.Setup(a => a.CadastrarAtendente(It.IsAny<Atendente>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var atendenteService = new AtendenteService(this.atendenteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.usuarioRepositoryMock.Object);

            // when
            var resultado = atendenteService.CadastrarAtendente(atendenteCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void CadastrarAtendenteComTelefoneSemMascaraTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendenteCadastro = new AtendenteCadastroViewModel("Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "34985433241", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendente = new Atendente(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "34985433241", endereco.IdEndereco);
            var usuarioAtendente = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            this.atendenteRepositoryMock.SetupSequence(a => a.BuscarAtendentePorCpf(atendenteCadastro.Cpf)).Returns((Atendente)null).Returns(atendente);
            this.atendenteRepositoryMock.Setup(a => a.BuscarAtendentePorRg(atendenteCadastro.Rg)).Returns((Atendente)null);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(atendenteCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.atendenteRepositoryMock.Setup(a => a.CadastrarAtendente(It.IsAny<Atendente>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var atendenteService = new AtendenteService(this.atendenteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.usuarioRepositoryMock.Object);

            // when
            var resultado = atendenteService.CadastrarAtendente(atendenteCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoCadastrarAtendenteComTelefoneInvalidoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendenteCadastro = new AtendenteCadastroViewModel("Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(FF)98543-3241", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendente = new Atendente(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(FF)98543-3241", endereco.IdEndereco);
            var usuarioAtendente = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            this.atendenteRepositoryMock.SetupSequence(a => a.BuscarAtendentePorCpf(atendenteCadastro.Cpf)).Returns((Atendente)null).Returns(atendente);
            this.atendenteRepositoryMock.Setup(a => a.BuscarAtendentePorRg(atendenteCadastro.Rg)).Returns((Atendente)null);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(atendenteCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.atendenteRepositoryMock.Setup(a => a.CadastrarAtendente(It.IsAny<Atendente>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var atendenteService = new AtendenteService(this.atendenteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.usuarioRepositoryMock.Object);

            // when
            var resultado = atendenteService.CadastrarAtendente(atendenteCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void CadastrarAtendenteComCepEnderecoSemMascaraTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente");
            var enderecoCadastro = new EnderecoViewModel("29500000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendenteCadastro = new AtendenteCadastroViewModel("Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendente = new Atendente(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", endereco.IdEndereco);
            var usuarioAtendente = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            this.atendenteRepositoryMock.SetupSequence(a => a.BuscarAtendentePorCpf(atendenteCadastro.Cpf)).Returns((Atendente)null).Returns(atendente);
            this.atendenteRepositoryMock.Setup(a => a.BuscarAtendentePorRg(atendenteCadastro.Rg)).Returns((Atendente)null);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(atendenteCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.atendenteRepositoryMock.Setup(a => a.CadastrarAtendente(It.IsAny<Atendente>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var atendenteService = new AtendenteService(this.atendenteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.usuarioRepositoryMock.Object);

            // when
            var resultado = atendenteService.CadastrarAtendente(atendenteCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoCadastrarAtendenteComCepEnderecoInvalidoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente");
            var enderecoCadastro = new EnderecoViewModel("29500", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendenteCadastro = new AtendenteCadastroViewModel("Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendente = new Atendente(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", endereco.IdEndereco);
            var usuarioAtendente = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            this.atendenteRepositoryMock.SetupSequence(a => a.BuscarAtendentePorCpf(atendenteCadastro.Cpf)).Returns((Atendente)null).Returns(atendente);
            this.atendenteRepositoryMock.Setup(a => a.BuscarAtendentePorRg(atendenteCadastro.Rg)).Returns((Atendente)null);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(atendenteCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.atendenteRepositoryMock.Setup(a => a.CadastrarAtendente(It.IsAny<Atendente>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var atendenteService = new AtendenteService(this.atendenteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.usuarioRepositoryMock.Object);

            // when
            var resultado = atendenteService.CadastrarAtendente(atendenteCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoCadastrarAtendenteComCpfRepetidoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendenteCadastro = new AtendenteCadastroViewModel("Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendente = new Atendente(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", endereco.IdEndereco);
            var usuarioAtendente = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            this.atendenteRepositoryMock.SetupSequence(a => a.BuscarAtendentePorCpf(atendenteCadastro.Cpf)).Returns(new Atendente()).Returns(atendente);
            this.atendenteRepositoryMock.Setup(a => a.BuscarAtendentePorRg(atendenteCadastro.Rg)).Returns((Atendente)null);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(atendenteCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.atendenteRepositoryMock.Setup(a => a.CadastrarAtendente(It.IsAny<Atendente>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var atendenteService = new AtendenteService(this.atendenteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.usuarioRepositoryMock.Object);

            // when
            var resultado = atendenteService.CadastrarAtendente(atendenteCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoCadastrarAtendenteComRgRepetidoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendenteCadastro = new AtendenteCadastroViewModel("Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendente = new Atendente(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", endereco.IdEndereco);
            var usuarioAtendente = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            this.atendenteRepositoryMock.SetupSequence(a => a.BuscarAtendentePorCpf(atendenteCadastro.Cpf)).Returns((Atendente)null).Returns(atendente);
            this.atendenteRepositoryMock.Setup(a => a.BuscarAtendentePorRg(atendenteCadastro.Rg)).Returns(new Atendente());
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(atendenteCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.atendenteRepositoryMock.Setup(a => a.CadastrarAtendente(It.IsAny<Atendente>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var atendenteService = new AtendenteService(this.atendenteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.usuarioRepositoryMock.Object);

            // when
            var resultado = atendenteService.CadastrarAtendente(atendenteCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoCadastrarAtendenteComEmailUsuarioRepetidoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendenteCadastro = new AtendenteCadastroViewModel("Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var atendente = new Atendente(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Joana", new DateTime(1980, 2, 5), "F", "122.457.789-12", "12.345.678-1", "joana@email.com", "(34)98543-3241", endereco.IdEndereco);
            var usuarioAtendente = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            this.atendenteRepositoryMock.SetupSequence(a => a.BuscarAtendentePorCpf(atendenteCadastro.Cpf)).Returns((Atendente)null).Returns(atendente);
            this.atendenteRepositoryMock.Setup(a => a.BuscarAtendentePorRg(atendenteCadastro.Rg)).Returns((Atendente)null);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(atendenteCadastro.Usuario.Email)).Returns(new Usuario());
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.atendenteRepositoryMock.Setup(a => a.CadastrarAtendente(It.IsAny<Atendente>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var atendenteService = new AtendenteService(this.atendenteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.usuarioRepositoryMock.Object);

            // when
            var resultado = atendenteService.CadastrarAtendente(atendenteCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }
    }
}
