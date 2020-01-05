using ConsultorioMedico.Application.Service;
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
    public class TesteUnitarioUsuarioService
    {
        private readonly Mock<IUsuarioRepository> usuarioRepositoryMock;
        private readonly Mock<IAtendenteRepository> atendenteRepositoryMock;
        private readonly Mock<IMedicoRepository> medicoRepositoryMock;
        private readonly Mock<IAgendamentoRepository> agendamentoRepositoryMock;

        public TesteUnitarioUsuarioService()
        {
            this.usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            this.atendenteRepositoryMock = new Mock<IAtendenteRepository>();
            this.medicoRepositoryMock = new Mock<IMedicoRepository>();
            this.agendamentoRepositoryMock = new Mock<IAgendamentoRepository>();
        }

        [Fact]
        public void ValidarUsuarioTest()
        {
            // given
            var endereco1 = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var endereco2 = new Endereco(new Guid("C15854DB-F4D9-465F-F66E-08D78D5509A2"), "29500-000", "Rua velha", "123", "Casa", "Centro", "Alegre", "ES");

            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco1.IdEndereco);
            var atendente = new Atendente(new Guid("FB30E734-7278-44B0-CA72-08D79091CA61"), "Joana", new DateTime(1988, 7, 15), "F", "125.453.345-32", "15.654.342-1", "joana@email.com", "(31)32434-3242", endereco2.IdEndereco);

            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);
            var usuarioAdmin = new Usuario(new Guid("418A3CF2-A78F-4AD2-84C6-712638AD048B"), "admin@email.com", "25d55ad283aa400af464c76d713c07ad", "Administrador", null, null);
            var usuarioAtendente = new Usuario(new Guid("3EE42C97-77B5-43B3-F5E1-08D79091CA56"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            this.usuarioRepositoryMock.Setup(u => u.VerificarExistenciaUsuario("marcos@email.com", "4f5f282e7e716424bcd5b5a10a82d7acabc87a0ae07ee88d9fd8ae69bbfbbfc9")).Returns(usuarioMedico);
            this.usuarioRepositoryMock.Setup(u => u.VerificarExistenciaUsuario("admin@email.com", "4f5f282e7e716424bcd5b5a10a82d7acabc87a0ae07ee88d9fd8ae69bbfbbfc9")).Returns(usuarioAdmin);
            this.usuarioRepositoryMock.Setup(u => u.VerificarExistenciaUsuario("joana@email.com", "4f5f282e7e716424bcd5b5a10a82d7acabc87a0ae07ee88d9fd8ae69bbfbbfc9")).Returns(usuarioAtendente);

            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object, atendenteRepositoryMock.Object, medicoRepositoryMock.Object, agendamentoRepositoryMock.Object);

            // when
            var usuarioLogadoMedico = usuarioService.ValidarUsuario("marcos@email.com", "25d55ad283aa400af464c76d713c07ad");
            var usuarioLogadoAdmin = usuarioService.ValidarUsuario("admin@email.com", "25d55ad283aa400af464c76d713c07ad");
            var usuarioLogadoAtendente = usuarioService.ValidarUsuario("joana@email.com", "25d55ad283aa400af464c76d713c07ad");
            var usuarioLogadoDesconhecido = usuarioService.ValidarUsuario("emailqualquer@email.com", "25d55ad283aa400af464c76d713c07ad");

            // then
            Assert.NotNull(usuarioLogadoMedico);
            Assert.True(usuarioLogadoMedico.Id.Equals(medico.IdMedico.ToString()));
            Assert.True(usuarioLogadoMedico.Tipo.Equals("Médico"));

            Assert.NotNull(usuarioLogadoAdmin);
            Assert.True(usuarioLogadoAdmin.Id.Equals(Guid.Empty.ToString()));
            Assert.True(usuarioLogadoAdmin.Tipo.Equals("Administrador"));

            Assert.NotNull(usuarioLogadoAtendente);
            Assert.True(usuarioLogadoAtendente.Id.Equals(atendente.IdAtendente.ToString()));
            Assert.True(usuarioLogadoAtendente.Tipo.Equals("Atendente"));

            Assert.Null(usuarioLogadoDesconhecido);
        }

        [Fact]
        public void ObterTodosUsuariosTest()
        {
            // given
            var endereco1 = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var endereco2 = new Endereco(new Guid("C15854DB-F4D9-465F-F66E-08D78D5509A2"), "29500-000", "Rua velha", "123", "Casa", "Centro", "Alegre", "ES");

            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco1.IdEndereco);
            var atendente = new Atendente(new Guid("FB30E734-7278-44B0-CA72-08D79091CA61"), "Joana", new DateTime(1988, 7, 15), "F", "125.453.345-32", "15.654.342-1", "joana@email.com", "(31)32434-3242", endereco2.IdEndereco);

            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);
            var usuarioAdmin = new Usuario(new Guid("418A3CF2-A78F-4AD2-84C6-712638AD048B"), "admin@email.com", "25d55ad283aa400af464c76d713c07ad", "Administrador", null, null);
            var usuarioAtendente = new Usuario(new Guid("3EE42C97-77B5-43B3-F5E1-08D79091CA56"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            var listaUsuarios = new List<Usuario>();
            listaUsuarios.Add(usuarioMedico);
            listaUsuarios.Add(usuarioAdmin);
            listaUsuarios.Add(usuarioAtendente);

            this.usuarioRepositoryMock.Setup(u => u.ObterTodosUsuarios()).Returns(listaUsuarios);

            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object, atendenteRepositoryMock.Object, medicoRepositoryMock.Object, agendamentoRepositoryMock.Object);

            // when
            var listaEncontrados = new List<UsuarioListarViewModel>(usuarioService.ObterTodosUsuarios());

            // then
            Assert.NotNull(listaEncontrados);
            Assert.True(listaEncontrados.Count > 0);
        }

        [Fact]
        public void DeletarUsuarioMedicoSemAgendamentoTest()
        {
            // given
            var endereco1 = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            
            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco1.IdEndereco);
            
            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorId(usuarioMedico.IdUsuario)).Returns(usuarioMedico);
            this.usuarioRepositoryMock.Setup(u => u.DeletarUsuario(usuarioMedico)).Returns(true);
            this.agendamentoRepositoryMock.Setup(a => a.QuantidadeAgendamentosMedico(medico.IdMedico)).Returns(0);
            this.medicoRepositoryMock.Setup(m => m.DeletarMedico(medico)).Returns(true);

            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object, atendenteRepositoryMock.Object, medicoRepositoryMock.Object, agendamentoRepositoryMock.Object);

            // when
            var mensagem = usuarioService.DeletarUsuario(usuarioMedico.IdUsuario.ToString());

            // then
            Assert.True(mensagem.Id == 1);
        }

        [Fact]
        public void DeletarUsuarioMedicoComAgendamentoTest()
        {
            // given
            var endereco1 = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");

            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco1.IdEndereco);

            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorId(usuarioMedico.IdUsuario)).Returns(usuarioMedico);
            this.usuarioRepositoryMock.Setup(u => u.DeletarUsuario(usuarioMedico)).Returns(true);
            this.agendamentoRepositoryMock.Setup(a => a.QuantidadeAgendamentosMedico(medico.IdMedico)).Returns(2);
            this.medicoRepositoryMock.Setup(m => m.AtualizarMedico(medico)).Returns(true);
            this.medicoRepositoryMock.Setup(m => m.DeletarMedico(medico)).Returns(true);

            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object, atendenteRepositoryMock.Object, medicoRepositoryMock.Object, agendamentoRepositoryMock.Object);

            // when
            var mensagem = usuarioService.DeletarUsuario(usuarioMedico.IdUsuario.ToString());

            // then
            Assert.True(mensagem.Id == 1);
        }

        [Fact]
        public void DeletarUsuarioAtendenteTest()
        {
            // given
            var endereco1 = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");

            var atendente = new Atendente(new Guid("FB30E734-7278-44B0-CA72-08D79091CA61"), "Joana", new DateTime(1988, 7, 15), "F", "125.453.345-32", "15.654.342-1", "joana@email.com", "(31)32434-3242", endereco1.IdEndereco);

            var usuarioAtendente = new Usuario(new Guid("3EE42C97-77B5-43B3-F5E1-08D79091CA56"), "joana@email.com", "25d55ad283aa400af464c76d713c07ad", "Atendente", null, atendente);

            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorId(usuarioAtendente.IdUsuario)).Returns(usuarioAtendente);
            this.usuarioRepositoryMock.Setup(u => u.DeletarUsuario(usuarioAtendente)).Returns(true);
            this.atendenteRepositoryMock.Setup(a => a.DeletarAtendente(usuarioAtendente.Atendente)).Returns(true);

            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object, atendenteRepositoryMock.Object, medicoRepositoryMock.Object, agendamentoRepositoryMock.Object);

            // when
            var mensagem = usuarioService.DeletarUsuario(usuarioAtendente.IdUsuario.ToString());

            // then
            Assert.True(mensagem.Id == 1);
        }

        [Fact]
        public void DeletarUsuarioAdminTest()
        {
            // given
            var usuarioAdmin = new Usuario(new Guid("418A3CF2-A78F-4AD2-84C6-712638AD048B"), "admin@email.com", "25d55ad283aa400af464c76d713c07ad", "Administrador", null, null);

            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorId(usuarioAdmin.IdUsuario)).Returns(usuarioAdmin);

            var usuarioService = new UsuarioService(usuarioRepositoryMock.Object, atendenteRepositoryMock.Object, medicoRepositoryMock.Object, agendamentoRepositoryMock.Object);

            // when
            var mensagem = usuarioService.DeletarUsuario(usuarioAdmin.IdUsuario.ToString());

            // then
            Assert.False(mensagem.Id == 1);
        }
    }
}
