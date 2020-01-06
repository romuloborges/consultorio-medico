using ConsultorioMedico.Application.Service;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Medico;
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
    public class TesteUnitarioMedicoService
    {
        private readonly Mock<IMedicoRepository> medicoRepositoryMock;
        private readonly Mock<IUsuarioRepository> usuarioRepositoryMock;
        private readonly Mock<IEnderecoRepository> enderecoRepositoryMock;
        public TesteUnitarioMedicoService()
        {
            this.medicoRepositoryMock = new Mock<IMedicoRepository>();
            this.usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            this.enderecoRepositoryMock = new Mock<IEnderecoRepository>();
        }

        [Fact]
        public void CadastrarMedicoComEnderecoNovoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medicoCadastro = new MedicoCadastroViewModel("Marcos", "123.456.789-12", "12.345.678-1", "1234567", new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco.IdEndereco);
            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorCpf(medicoCadastro.Cpf)).Returns((Medico) null);
            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorRg(medicoCadastro.Rg)).Returns((Medico) null);
            this.medicoRepositoryMock.SetupSequence(m => m.BuscarMedicoPorCrm(int.Parse(medicoCadastro.Crm))).Returns((Medico) null).Returns(medico);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(medicoCadastro.Usuario.Email)).Returns((Usuario) null);
            this.enderecoRepositoryMock.SetupSequence(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.medicoRepositoryMock.Setup(m => m.CadastrarMedico(It.IsAny<Medico>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var medicoService = new MedicoService(this.medicoRepositoryMock.Object, this.usuarioRepositoryMock.Object, this.enderecoRepositoryMock.Object);

            // when
            var resultado = medicoService.CadastrarMedico(medicoCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void CadastrarMedicoComEnderecoExistenteTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medicoCadastro = new MedicoCadastroViewModel("Marcos", "123.456.789-12", "12.345.678-1", "1234567", new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco.IdEndereco);
            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorCpf(medicoCadastro.Cpf)).Returns((Medico)null);
            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorRg(medicoCadastro.Rg)).Returns((Medico)null);
            this.medicoRepositoryMock.SetupSequence(m => m.BuscarMedicoPorCrm(int.Parse(medicoCadastro.Crm))).Returns((Medico)null).Returns(medico);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(medicoCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.medicoRepositoryMock.Setup(m => m.CadastrarMedico(It.IsAny<Medico>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var medicoService = new MedicoService(this.medicoRepositoryMock.Object, this.usuarioRepositoryMock.Object, this.enderecoRepositoryMock.Object);

            // when
            var resultado = medicoService.CadastrarMedico(medicoCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void CadastrarMedicoComCpfSemMascara()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medicoCadastro = new MedicoCadastroViewModel("Marcos", "12345678912", "12.345.678-1", "1234567", new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "12345678912", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco.IdEndereco);
            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorCpf(medicoCadastro.Cpf)).Returns((Medico)null);
            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorRg(medicoCadastro.Rg)).Returns((Medico)null);
            this.medicoRepositoryMock.SetupSequence(m => m.BuscarMedicoPorCrm(int.Parse(medicoCadastro.Crm))).Returns((Medico)null).Returns(medico);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(medicoCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.medicoRepositoryMock.Setup(m => m.CadastrarMedico(It.IsAny<Medico>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var medicoService = new MedicoService(this.medicoRepositoryMock.Object, this.usuarioRepositoryMock.Object, this.enderecoRepositoryMock.Object);

            // when
            var resultado = medicoService.CadastrarMedico(medicoCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoCadastrarMedicoComCpfInvalido()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medicoCadastro = new MedicoCadastroViewModel("Marcos", "1234567", "12.345.678-1", "1234567", new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "1234567", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco.IdEndereco);
            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorCpf(medicoCadastro.Cpf)).Returns((Medico)null);
            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorRg(medicoCadastro.Rg)).Returns((Medico)null);
            this.medicoRepositoryMock.SetupSequence(m => m.BuscarMedicoPorCrm(int.Parse(medicoCadastro.Crm))).Returns((Medico)null).Returns(medico);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(medicoCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.medicoRepositoryMock.Setup(m => m.CadastrarMedico(It.IsAny<Medico>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var medicoService = new MedicoService(this.medicoRepositoryMock.Object, this.usuarioRepositoryMock.Object, this.enderecoRepositoryMock.Object);

            // when
            var resultado = medicoService.CadastrarMedico(medicoCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void CadastrarMedicoComRgSemMascaraTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medicoCadastro = new MedicoCadastroViewModel("Marcos", "123.456.789-12", "123456781", "1234567", new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "123456781", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco.IdEndereco);
            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorCpf(medicoCadastro.Cpf)).Returns((Medico)null);
            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorRg(medicoCadastro.Rg)).Returns((Medico)null);
            this.medicoRepositoryMock.SetupSequence(m => m.BuscarMedicoPorCrm(int.Parse(medicoCadastro.Crm))).Returns((Medico)null).Returns(medico);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(medicoCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.medicoRepositoryMock.Setup(m => m.CadastrarMedico(It.IsAny<Medico>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var medicoService = new MedicoService(this.medicoRepositoryMock.Object, this.usuarioRepositoryMock.Object, this.enderecoRepositoryMock.Object);

            // when
            var resultado = medicoService.CadastrarMedico(medicoCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoCadastrarMedicoComRgInvalidoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medicoCadastro = new MedicoCadastroViewModel("Marcos", "123.456.789-12", "12.345.67A-2", "1234567", new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "12.345.67A-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco.IdEndereco);
            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorCpf(medicoCadastro.Cpf)).Returns((Medico)null);
            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorRg(medicoCadastro.Rg)).Returns((Medico)null);
            this.medicoRepositoryMock.SetupSequence(m => m.BuscarMedicoPorCrm(int.Parse(medicoCadastro.Crm))).Returns((Medico)null).Returns(medico);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(medicoCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.medicoRepositoryMock.Setup(m => m.CadastrarMedico(It.IsAny<Medico>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var medicoService = new MedicoService(this.medicoRepositoryMock.Object, this.usuarioRepositoryMock.Object, this.enderecoRepositoryMock.Object);

            // when
            var resultado = medicoService.CadastrarMedico(medicoCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void CadastrarMedicoComTelefoneSemMascaraTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medicoCadastro = new MedicoCadastroViewModel("Marcos", "123.456.789-12", "12.345.678-1", "1234567", new DateTime(1980, 2, 5), "M", "34985433241", "marcos@email.com", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "34985433241", "marcos@email.com", endereco.IdEndereco);
            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorCpf(medicoCadastro.Cpf)).Returns((Medico)null);
            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorRg(medicoCadastro.Rg)).Returns((Medico)null);
            this.medicoRepositoryMock.SetupSequence(m => m.BuscarMedicoPorCrm(int.Parse(medicoCadastro.Crm))).Returns((Medico)null).Returns(medico);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(medicoCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.medicoRepositoryMock.Setup(m => m.CadastrarMedico(It.IsAny<Medico>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var medicoService = new MedicoService(this.medicoRepositoryMock.Object, this.usuarioRepositoryMock.Object, this.enderecoRepositoryMock.Object);

            // when
            var resultado = medicoService.CadastrarMedico(medicoCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoCadastrarMedicoComTelefoneInvalidoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medicoCadastro = new MedicoCadastroViewModel("Marcos", "123.456.789-12", "12.345.678-1", "1234567", new DateTime(1980, 2, 5), "M", "(34985433241", "marcos@email.com", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34985433241", "marcos@email.com", endereco.IdEndereco);
            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorCpf(medicoCadastro.Cpf)).Returns((Medico)null);
            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorRg(medicoCadastro.Rg)).Returns((Medico)null);
            this.medicoRepositoryMock.SetupSequence(m => m.BuscarMedicoPorCrm(int.Parse(medicoCadastro.Crm))).Returns((Medico)null).Returns(medico);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(medicoCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.medicoRepositoryMock.Setup(m => m.CadastrarMedico(It.IsAny<Medico>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var medicoService = new MedicoService(this.medicoRepositoryMock.Object, this.usuarioRepositoryMock.Object, this.enderecoRepositoryMock.Object);

            // when
            var resultado = medicoService.CadastrarMedico(medicoCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void CadastrarMedicoComCepEnderecoSemMascaraTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico");
            var enderecoCadastro = new EnderecoViewModel("29500000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medicoCadastro = new MedicoCadastroViewModel("Marcos", "123.456.789-12", "12.345.678-1", "1234567", new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco.IdEndereco);
            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorCpf(medicoCadastro.Cpf)).Returns((Medico)null);
            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorRg(medicoCadastro.Rg)).Returns((Medico)null);
            this.medicoRepositoryMock.SetupSequence(m => m.BuscarMedicoPorCrm(int.Parse(medicoCadastro.Crm))).Returns((Medico)null).Returns(medico);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(medicoCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.medicoRepositoryMock.Setup(m => m.CadastrarMedico(It.IsAny<Medico>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var medicoService = new MedicoService(this.medicoRepositoryMock.Object, this.usuarioRepositoryMock.Object, this.enderecoRepositoryMock.Object);

            // when
            var resultado = medicoService.CadastrarMedico(medicoCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoCadastrarMedicoComCepEnderecoInvalidoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico");
            var enderecoCadastro = new EnderecoViewModel("29500", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medicoCadastro = new MedicoCadastroViewModel("Marcos", "123.456.789-12", "12.345.678-1", "1234567", new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco.IdEndereco);
            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorCpf(medicoCadastro.Cpf)).Returns((Medico)null);
            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorRg(medicoCadastro.Rg)).Returns((Medico)null);
            this.medicoRepositoryMock.SetupSequence(m => m.BuscarMedicoPorCrm(int.Parse(medicoCadastro.Crm))).Returns((Medico)null).Returns(medico);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(medicoCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.medicoRepositoryMock.Setup(m => m.CadastrarMedico(It.IsAny<Medico>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var medicoService = new MedicoService(this.medicoRepositoryMock.Object, this.usuarioRepositoryMock.Object, this.enderecoRepositoryMock.Object);

            // when
            var resultado = medicoService.CadastrarMedico(medicoCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoCadastrarMedicoComCpfRepetidoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medicoCadastro = new MedicoCadastroViewModel("Marcos", "123.456.789-12", "12.345.678-1", "1234567", new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco.IdEndereco);
            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorCpf(medicoCadastro.Cpf)).Returns(new Medico());
            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorRg(medicoCadastro.Rg)).Returns((Medico)null);
            this.medicoRepositoryMock.SetupSequence(m => m.BuscarMedicoPorCrm(int.Parse(medicoCadastro.Crm))).Returns((Medico)null).Returns(medico);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(medicoCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.medicoRepositoryMock.Setup(m => m.CadastrarMedico(It.IsAny<Medico>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var medicoService = new MedicoService(this.medicoRepositoryMock.Object, this.usuarioRepositoryMock.Object, this.enderecoRepositoryMock.Object);

            // when
            var resultado = medicoService.CadastrarMedico(medicoCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoCadastrarMedicoComCrmRepetidoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medicoCadastro = new MedicoCadastroViewModel("Marcos", "123.456.789-12", "12.345.678-1", "1234567", new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco.IdEndereco);
            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorCpf(medicoCadastro.Cpf)).Returns((Medico)null);
            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorRg(medicoCadastro.Rg)).Returns((Medico)null);
            this.medicoRepositoryMock.SetupSequence(m => m.BuscarMedicoPorCrm(int.Parse(medicoCadastro.Crm))).Returns(new Medico()).Returns(medico);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(medicoCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.medicoRepositoryMock.Setup(m => m.CadastrarMedico(It.IsAny<Medico>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var medicoService = new MedicoService(this.medicoRepositoryMock.Object, this.usuarioRepositoryMock.Object, this.enderecoRepositoryMock.Object);

            // when
            var resultado = medicoService.CadastrarMedico(medicoCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoCadastrarMedicoComRgRepetidoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medicoCadastro = new MedicoCadastroViewModel("Marcos", "123.456.789-12", "12.345.678-1", "1234567", new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco.IdEndereco);
            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorCpf(medicoCadastro.Cpf)).Returns((Medico)null);
            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorRg(medicoCadastro.Rg)).Returns(new Medico());
            this.medicoRepositoryMock.SetupSequence(m => m.BuscarMedicoPorCrm(int.Parse(medicoCadastro.Crm))).Returns((Medico)null).Returns(medico);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(medicoCadastro.Usuario.Email)).Returns((Usuario)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.medicoRepositoryMock.Setup(m => m.CadastrarMedico(It.IsAny<Medico>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var medicoService = new MedicoService(this.medicoRepositoryMock.Object, this.usuarioRepositoryMock.Object, this.enderecoRepositoryMock.Object);

            // when
            var resultado = medicoService.CadastrarMedico(medicoCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoCadastrarMedicoComEmailUsuarioRepetidoTest()
        {
            // given
            var usuarioCadastro = new UsuarioCadastroViewModel("marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico");
            var enderecoCadastro = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medicoCadastro = new MedicoCadastroViewModel("Marcos", "123.456.789-12", "12.345.678-1", "1234567", new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", enderecoCadastro, usuarioCadastro);

            var endereco = new Endereco(new Guid("1EF2F5CB-A04B-4761-3C44-08D78CC135ED"), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var medico = new Medico(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "Marcos", "123.456.789-12", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", endereco.IdEndereco);
            var usuarioMedico = new Usuario(new Guid("1A7C25A0-896F-49DF-A75E-EE7DD53AECB9"), "marcos@email.com", "25d55ad283aa400af464c76d713c07ad", "Médico", medico, null);

            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorCpf(medicoCadastro.Cpf)).Returns((Medico)null);
            this.medicoRepositoryMock.Setup(m => m.BuscarMedicoPorRg(medicoCadastro.Rg)).Returns((Medico)null);
            this.medicoRepositoryMock.SetupSequence(m => m.BuscarMedicoPorCrm(int.Parse(medicoCadastro.Crm))).Returns((Medico)null).Returns(medico);
            this.usuarioRepositoryMock.Setup(u => u.ObterUsuarioPorEmail(medicoCadastro.Usuario.Email)).Returns(new Usuario());
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);
            this.medicoRepositoryMock.Setup(m => m.CadastrarMedico(It.IsAny<Medico>())).Returns(true);
            this.usuarioRepositoryMock.Setup(u => u.CadastrarUsuario(It.IsAny<Usuario>())).Returns(true);

            var medicoService = new MedicoService(this.medicoRepositoryMock.Object, this.usuarioRepositoryMock.Object, this.enderecoRepositoryMock.Object);

            // when
            var resultado = medicoService.CadastrarMedico(medicoCadastro);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void ObterTodosMedicosParaMatSelectTest()
        {
            // given
            Medico medico1 = new Medico(Guid.NewGuid(), "Marcos", "123.456.789-12", "12.345.678-1", 1234567, new DateTime(1980, 2, 5), "M", "(34)98543-3241", "marcos@email.com", true, Guid.NewGuid());
            Medico medico2 = new Medico(Guid.NewGuid(), "Joana", "125.456.719-12", "11.345.678-9", 1233567, new DateTime(1980, 9, 1), "F", "(35)91543-3241", "joana@email.com", true, Guid.NewGuid());

            var listaMedicos = new List<Medico>();
            listaMedicos.Add(medico1);
            listaMedicos.Add(medico2);

            this.medicoRepositoryMock.Setup(m => m.ObterTodosMedicosAtivosSemEndereco()).Returns(listaMedicos);

            var medicoService = new MedicoService(this.medicoRepositoryMock.Object, this.usuarioRepositoryMock.Object, this.enderecoRepositoryMock.Object);

            // when
            var listaMedicosRetorno = new List<MedicoMatSelectViewModel>(medicoService.ObterTodosMedicosParaMatSelect());

            // then
            Assert.NotNull(listaMedicosRetorno);
            Assert.True(listaMedicosRetorno.Count == listaMedicos.Count);
        }
    }
}
