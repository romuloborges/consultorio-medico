using ConsultorioMedico.Application.Service;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Endereco;
using ConsultorioMedico.Application.ViewModel.Paciente;
using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConsultorioMedico_Backend.Test
{
    public class TesteUnitarioPacienteService
    {
        private readonly Mock<IPacienteRepository> pacienteRepositoryMock;
        private readonly Mock<IEnderecoRepository> enderecoRepositoryMock;
        private readonly Mock<IAgendamentoRepository> agendamentoRepositoryMock;
        private readonly Mock<IConsultaRepository> consultaRepositoryMock;

        public TesteUnitarioPacienteService()
        {
            this.pacienteRepositoryMock = new Mock<IPacienteRepository>();
            this.enderecoRepositoryMock = new Mock<IEnderecoRepository>();
            this.agendamentoRepositoryMock = new Mock<IAgendamentoRepository>();
            this.consultaRepositoryMock = new Mock<IConsultaRepository>();
        }

        [Fact]
        public void CadastrarPacienteComEnderecoExistenteTest()
        {
            // given
            var endereco1 = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteCadastrarViewModel("Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente) null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente) null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(new Guid("C62ACB1E-94E1-487F-0F68-08D79090D2CB"));
            this.pacienteRepositoryMock.Setup(p => p.CadastrarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.CadastrarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void CadastrarPacienteComNovoEnderecoTest()
        {
            // given
            var endereco1 = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteCadastrarViewModel("Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.SetupSequence(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);

            this.pacienteRepositoryMock.Setup(p => p.CadastrarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.CadastrarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void CadastrarPacienteComCpfSemMascaraTest()
        {
            // given
            var endereco1 = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteCadastrarViewModel("Joao", "", DateTime.Now, "M", "12345678912", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.SetupSequence(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);

            this.pacienteRepositoryMock.Setup(p => p.CadastrarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.CadastrarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoCadastrarPacienteComCpfInvalidoTest()
        {
            // given
            var endereco1 = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteCadastrarViewModel("Joao", "", DateTime.Now, "M", "12345678912430", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.SetupSequence(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);

            this.pacienteRepositoryMock.Setup(p => p.CadastrarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.CadastrarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void CadastrarPacienteComRgSemMascaraTest()
        {
            // given
            var endereco1 = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteCadastrarViewModel("Joao", "", DateTime.Now, "M", "123.456.789-12", "121234561", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.SetupSequence(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);

            this.pacienteRepositoryMock.Setup(p => p.CadastrarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.CadastrarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoCadastrarPacienteComRgInvalidoTest()
        {
            // given
            var endereco1 = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteCadastrarViewModel("Joao", "", DateTime.Now, "M", "123.456.789-12", "1212", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.SetupSequence(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);

            this.pacienteRepositoryMock.Setup(p => p.CadastrarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.CadastrarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void CadastrarPacienteComTelefoneSemMascaraTest()
        {
            // given
            var endereco1 = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteCadastrarViewModel("Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "21987645433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.SetupSequence(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);

            this.pacienteRepositoryMock.Setup(p => p.CadastrarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.CadastrarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoCadastrarPacienteComTelefoneInvalidoTest()
        {
            // given
            var endereco1 = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteCadastrarViewModel("Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "219a76F543", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.SetupSequence(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);

            this.pacienteRepositoryMock.Setup(p => p.CadastrarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.CadastrarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void CadastrarPacienteComCepEnderecoSemMascaraTest()
        {
            // given
            var endereco1 = new EnderecoViewModel("29500000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteCadastrarViewModel("Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.SetupSequence(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);

            this.pacienteRepositoryMock.Setup(p => p.CadastrarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.CadastrarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoCadastrarPacienteComCepEnderecoInvalidoTest()
        {
            // given
            var endereco1 = new EnderecoViewModel("29500", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteCadastrarViewModel("Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.SetupSequence(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);

            this.pacienteRepositoryMock.Setup(p => p.CadastrarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.CadastrarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoCadastrarPacienteComCpfRepetidoTest()
        {
            // given
            var endereco1 = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteCadastrarViewModel("Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns(new Paciente());
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.SetupSequence(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);

            this.pacienteRepositoryMock.Setup(p => p.CadastrarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.CadastrarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoCadastrarPacienteComRgRepetidoTest()
        {
            // given
            var endereco1 = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteCadastrarViewModel("Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns(new Paciente());
            this.enderecoRepositoryMock.SetupSequence(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);

            this.pacienteRepositoryMock.Setup(p => p.CadastrarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.CadastrarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoCadastrarPacienteComDataNascimentoInvalidaTest()
        {
            // given
            var endereco1 = new EnderecoViewModel("29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteCadastrarViewModel("Joao", "", DateTime.Now.AddDays(1), "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.SetupSequence(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);

            this.pacienteRepositoryMock.Setup(p => p.CadastrarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.CadastrarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void AtualizarPacienteNovoEnderecoComDependenteTest()
        {
            // given
            var endereco1 = new EnderecoListarEditarViewModel(Guid.NewGuid().ToString(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteListarEditarViewModel(Guid.NewGuid().ToString(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.SetupSequence(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty).Returns(Guid.NewGuid());
            this.enderecoRepositoryMock.Setup(e => e.QuantidadeReferenciasEndereco(It.IsAny<Guid>())).Returns(2);
            this.enderecoRepositoryMock.Setup(e => e.CadastrarEndereco(It.IsAny<Endereco>())).Returns(true);

            this.pacienteRepositoryMock.Setup(p => p.AtualizarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.AtualizarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void AtualizarPacienteNovoEnderecoSemDependenteTest()
        {
            // given
            var endereco1 = new EnderecoListarEditarViewModel(Guid.NewGuid().ToString(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteListarEditarViewModel(Guid.NewGuid().ToString(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.Empty);
            this.enderecoRepositoryMock.Setup(e => e.QuantidadeReferenciasEndereco(It.IsAny<Guid>())).Returns(1);
            this.enderecoRepositoryMock.Setup(e => e.AtualizarEndereco(It.IsAny<Endereco>())).Returns(true);

            this.pacienteRepositoryMock.Setup(p => p.AtualizarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.AtualizarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void AtualizarPacienteEnderecoExistenteTest()
        {
            // given
            var endereco1 = new EnderecoListarEditarViewModel(Guid.NewGuid().ToString(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteListarEditarViewModel(Guid.NewGuid().ToString(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());

            this.pacienteRepositoryMock.Setup(p => p.AtualizarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.AtualizarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void AtualizarPacienteComCpfSemMascaraTest()
        {
            // given
            var endereco1 = new EnderecoListarEditarViewModel(Guid.NewGuid().ToString(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteListarEditarViewModel(Guid.NewGuid().ToString(), "Joao", "", DateTime.Now, "M", "12345678912", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());

            this.pacienteRepositoryMock.Setup(p => p.AtualizarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.AtualizarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoAtualizarPacienteComCpfInvalidoTest()
        {
            // given
            var endereco1 = new EnderecoListarEditarViewModel(Guid.NewGuid().ToString(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteListarEditarViewModel(Guid.NewGuid().ToString(), "Joao", "", DateTime.Now, "M", "123.456.789-123", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());

            this.pacienteRepositoryMock.Setup(p => p.AtualizarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.AtualizarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void AtualizarPacienteComRgSemMascaraTest()
        {
            // given
            var endereco1 = new EnderecoListarEditarViewModel(Guid.NewGuid().ToString(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteListarEditarViewModel(Guid.NewGuid().ToString(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "121234561", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());

            this.pacienteRepositoryMock.Setup(p => p.AtualizarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.AtualizarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoAtualizarPacienteComRgInvalidoTest()
        {
            // given
            var endereco1 = new EnderecoListarEditarViewModel(Guid.NewGuid().ToString(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteListarEditarViewModel(Guid.NewGuid().ToString(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.4A61", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());

            this.pacienteRepositoryMock.Setup(p => p.AtualizarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.AtualizarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void AtualizarPacienteComTelefoneSemMascaraTest()
        {
            // given
            var endereco1 = new EnderecoListarEditarViewModel(Guid.NewGuid().ToString(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteListarEditarViewModel(Guid.NewGuid().ToString(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "21987645433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());

            this.pacienteRepositoryMock.Setup(p => p.AtualizarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.AtualizarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoAtualizarPacienteComTelefoneInvalidoTest()
        {
            // given
            var endereco1 = new EnderecoListarEditarViewModel(Guid.NewGuid().ToString(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteListarEditarViewModel(Guid.NewGuid().ToString(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98#6N5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());

            this.pacienteRepositoryMock.Setup(p => p.AtualizarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.AtualizarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void AtualizarPacienteComCepEnderecoSemMascaraTest()
        {
            // given
            var endereco1 = new EnderecoListarEditarViewModel(Guid.NewGuid().ToString(), "29500000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteListarEditarViewModel(Guid.NewGuid().ToString(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());

            this.pacienteRepositoryMock.Setup(p => p.AtualizarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.AtualizarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoAtualizarPacienteComCepEnderecoInvalidoTest()
        {
            // given
            var endereco1 = new EnderecoListarEditarViewModel(Guid.NewGuid().ToString(), "2950", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteListarEditarViewModel(Guid.NewGuid().ToString(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());

            this.pacienteRepositoryMock.Setup(p => p.AtualizarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.AtualizarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoAtualizarPacienteComCpfRepetidoTest()
        {
            // given
            var endereco1 = new EnderecoListarEditarViewModel(Guid.NewGuid().ToString(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteListarEditarViewModel(Guid.NewGuid().ToString(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns(new Paciente());
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());

            this.pacienteRepositoryMock.Setup(p => p.AtualizarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.AtualizarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoAtualizarPacienteComRgRepetidoTest()
        {
            // given
            var endereco1 = new EnderecoListarEditarViewModel(Guid.NewGuid().ToString(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteListarEditarViewModel(Guid.NewGuid().ToString(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns(new Paciente());
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());

            this.pacienteRepositoryMock.Setup(p => p.AtualizarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.AtualizarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoAtualizarPacienteComDataNascimentoInvalidaTest()
        {
            // given
            var endereco1 = new EnderecoListarEditarViewModel(Guid.NewGuid().ToString(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var pacienteViewModel = new PacienteListarEditarViewModel(Guid.NewGuid().ToString(), "Joao", "", DateTime.Now.AddDays(1), "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", endereco1);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorCpf(pacienteViewModel.Cpf)).Returns((Paciente)null);
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorRg(pacienteViewModel.Rg)).Returns((Paciente)null);
            this.enderecoRepositoryMock.Setup(e => e.BuscaIdEndereco(It.IsAny<Endereco>())).Returns(Guid.NewGuid());

            this.pacienteRepositoryMock.Setup(p => p.AtualizarPaciente(It.IsAny<Paciente>())).Returns(true);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.AtualizarPaciente(pacienteViewModel);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void DeletarPacienteSemAgendamentoTest()
        {
            // given
            var paciente = new Paciente(Guid.NewGuid(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "21.343.454-1", "(23)34454-1234", "joao@email.com", Guid.NewGuid());
            
            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorId(It.IsAny<Guid>())).Returns(paciente);
            this.pacienteRepositoryMock.Setup(p => p.DeletarPaciente(It.IsAny<Paciente>())).Returns(true);
            this.agendamentoRepositoryMock.Setup(a => a.ContarAgendamentosPaciente(It.IsAny<Guid>())).Returns(0);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.DeletarPaciente(paciente.IdPaciente.ToString());

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoDeletarPacienteComAgendamentoTest()
        {
            // given
            var paciente = new Paciente(Guid.NewGuid(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "21.343.454-1", "(23)34454-1234", "joao@email.com", Guid.NewGuid());

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorId(It.IsAny<Guid>())).Returns(paciente);
            this.pacienteRepositoryMock.Setup(p => p.DeletarPaciente(It.IsAny<Paciente>())).Returns(true);
            this.agendamentoRepositoryMock.Setup(a => a.ContarAgendamentosPaciente(It.IsAny<Guid>())).Returns(1);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.DeletarPaciente(paciente.IdPaciente.ToString());

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoDeletarPacienteComIdInvalidoTest()
        {
            // given
            var paciente = new Paciente(Guid.NewGuid(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "21.343.454-1", "(23)34454-1234", "joao@email.com", Guid.NewGuid());

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorId(It.IsAny<Guid>())).Returns((Paciente) null);
            this.pacienteRepositoryMock.Setup(p => p.DeletarPaciente(It.IsAny<Paciente>())).Returns(true);
            this.agendamentoRepositoryMock.Setup(a => a.ContarAgendamentosPaciente(It.IsAny<Guid>())).Returns(1);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.DeletarPaciente(paciente.IdPaciente.ToString());

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void ObterPacienteCompletoTest()
        {
            // given
            var endereco = new Endereco(Guid.NewGuid(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var paciente = new Paciente(Guid.NewGuid(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "21.343.454-1", "(23)34454-1234", "joao@email.com", endereco);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorId(It.IsAny<Guid>())).Returns(paciente);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = pacienteService.ObterPacienteCompleto(paciente.IdPaciente.ToString());

            // then
            Assert.NotNull(resultado);
        }

        [Fact]
        public void NaoObterPacienteCompletoIdInvalidoTest()
        {
            // given
            var endereco = new Endereco(Guid.NewGuid(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var paciente = new Paciente(Guid.NewGuid(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "21.343.454-1", "(23)34454-1234", "joao@email.com", endereco);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorId(It.IsAny<Guid>())).Returns((Paciente) null);

            var pacienteService = new PacienteService(this.pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var pacienteRetornado = pacienteService.ObterPacienteCompleto(paciente.IdPaciente.ToString());

            // then
            Assert.Null(pacienteRetornado);
        }

        [Fact]
        public void ObterPacienteConsultaTest()
        {
            // given            
            var endereco = new Endereco(Guid.NewGuid(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var paciente = new Paciente(Guid.NewGuid(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "21.343.454-1", "(23)34454-1234", "joao@email.com", endereco);

            this.pacienteRepositoryMock.Setup(x => x.BuscarPacientePorId(It.IsAny<Guid>())).Returns(paciente);
            
            var pacienteService = new PacienteService(pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var pacienteRetornado = pacienteService.ObterPacienteConsulta(Guid.NewGuid().ToString());

            //then
            Assert.NotNull(paciente);
            Assert.True(new Guid(pacienteRetornado.IdPaciente).Equals(paciente.IdPaciente));
        }

        [Fact]
        public void NaoObterPacienteConsultaIdInvalidoTest()
        {
            // given            
            var endereco = new Endereco(Guid.NewGuid(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var paciente = new Paciente(Guid.NewGuid(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "21.343.454-1", "(23)34454-1234", "joao@email.com", endereco);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorId(It.IsAny<Guid>())).Returns((Paciente) null);
            
            var pacienteService = new PacienteService(pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var pacienteRetornado = pacienteService.ObterPacienteConsulta(Guid.NewGuid().ToString());

            //then
            Assert.Null(pacienteRetornado);
        }

        [Fact]
        public void ObterPacienteParaRegistrarConsultaTest()
        {
            // given
            var endereco = new Endereco(Guid.NewGuid(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var paciente = new Paciente(Guid.NewGuid(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "21.343.454-1", "(23)34454-1234", "joao@email.com", endereco);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorId(It.IsAny<Guid>())).Returns(paciente);

            var pacienteService = new PacienteService(pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var pacienteRetornado = pacienteService.ObterPacienteParaRegistrarConsulta(Guid.NewGuid().ToString());

            // then
            Assert.NotNull(pacienteRetornado);
            Assert.True(pacienteRetornado.Cpf.Equals(paciente.Cpf));
        }

        [Fact]
        public void NaoObterPacienteParaRegistrarConsultaIdInvalidoTest()
        {
            // given
            var endereco = new Endereco(Guid.NewGuid(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var paciente = new Paciente(Guid.NewGuid(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "21.343.454-1", "(23)34454-1234", "joao@email.com", endereco);

            this.pacienteRepositoryMock.Setup(p => p.BuscarPacientePorId(It.IsAny<Guid>())).Returns((Paciente) null);

            var pacienteService = new PacienteService(pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var pacienteRetornado = pacienteService.ObterPacienteParaRegistrarConsulta(Guid.NewGuid().ToString());

            // then
            Assert.Null(pacienteRetornado);
        }

        [Fact]
        public void ObterPacientesComFiltroTest()
        {
            // given
            var endereco = new Endereco(Guid.NewGuid(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var paciente1 = new Paciente(Guid.NewGuid(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "21.343.454-1", "(23)34954-1334", "joao@email.com", endereco);
            var paciente2 = new Paciente(Guid.NewGuid(), "Joaquim", "", DateTime.Now, "M", "125.456.789-12", "27.343.454-1", "(23)34354-1234", "joaquim@email.com", endereco);
            var paciente3 = new Paciente(Guid.NewGuid(), "Jo", "", DateTime.Now, "M", "127.456.789-12", "22.343.454-1", "(23)34455-1834", "jo@email.com", endereco);

            var listaPacientes = new List<Paciente>();
            listaPacientes.Add(paciente1);
            listaPacientes.Add(paciente2);
            listaPacientes.Add(paciente3);

            this.pacienteRepositoryMock.Setup(p => p.ObterPacientesComFiltro("", "", DateTime.MinValue, DateTime.MinValue)).Returns(listaPacientes);
            this.consultaRepositoryMock.Setup(c => c.ContaConsultasPorPaciente(It.IsAny<Guid>())).Returns(1);
            this.agendamentoRepositoryMock.Setup(a => a.ContarAgendamentosPaciente(It.IsAny<Guid>())).Returns(2);

            var pacienteService = new PacienteService(pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var listaRetornada = new List<PacienteTabelaListarViewModel>(pacienteService.ObterPacientesComFiltroParaTabela("naoha", "naoha", DateTime.MinValue, DateTime.MinValue));

            // then
            Assert.NotNull(listaRetornada);
            Assert.True(listaRetornada.Count == listaPacientes.Count);
        }

        [Fact]
        public void ObterTodosPacientesParaTabelaTest()
        {
            // given
            var endereco = new Endereco(Guid.NewGuid(), "29500-000", "Rua nova", "123", "Casa", "Centro", "Alegre", "ES");
            var paciente1 = new Paciente(Guid.NewGuid(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "21.343.454-1", "(23)34954-1334", "joao@email.com", endereco);
            var paciente2 = new Paciente(Guid.NewGuid(), "Joaquim", "", DateTime.Now, "M", "125.456.789-12", "27.343.454-1", "(23)34354-1234", "joaquim@email.com", endereco);
            var paciente3 = new Paciente(Guid.NewGuid(), "Jo", "", DateTime.Now, "M", "127.456.789-12", "22.343.454-1", "(23)34455-1834", "jo@email.com", endereco);

            var listaPacientes = new List<Paciente>();
            listaPacientes.Add(paciente1);
            listaPacientes.Add(paciente2);
            listaPacientes.Add(paciente3);

            this.pacienteRepositoryMock.Setup(p => p.ObterTodosPacientesComEndereco()).Returns(listaPacientes);
            this.consultaRepositoryMock.Setup(c => c.ContaConsultasPorPaciente(It.IsAny<Guid>())).Returns(1);
            this.agendamentoRepositoryMock.Setup(a => a.ContarAgendamentosPaciente(It.IsAny<Guid>())).Returns(2);

            var pacienteService = new PacienteService(pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var listaRetornada = new List<PacienteTabelaListarViewModel>(pacienteService.ObterTodosPacientesParaTabela());

            // then
            Assert.NotNull(listaRetornada);
            Assert.True(listaRetornada.Count == listaPacientes.Count);
        }

        [Fact]
        public void ObterTodosPacientesParaMatSelectTest()
        {
            // given
            var paciente1 = new Paciente(Guid.NewGuid(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "21.343.454-1", "(23)34954-1334", "joao@email.com", null);
            var paciente2 = new Paciente(Guid.NewGuid(), "Joaquim", "", DateTime.Now, "M", "125.456.789-12", "27.343.454-1", "(23)34354-1234", "joaquim@email.com", null);
            var paciente3 = new Paciente(Guid.NewGuid(), "Jo", "", DateTime.Now, "M", "127.456.789-12", "22.343.454-1", "(23)34455-1834", "jo@email.com", null);

            var listaPacientes = new List<Paciente>();
            listaPacientes.Add(paciente1);
            listaPacientes.Add(paciente2);
            listaPacientes.Add(paciente3);

            this.pacienteRepositoryMock.Setup(p => p.ObterTodosPacientesSemEndereco()).Returns(listaPacientes);

            var pacienteService = new PacienteService(pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var listaRetornada = new List<PacienteMatSelect>(pacienteService.ObterTodosPacientesParaMatSelect());

            // then
            Assert.NotNull(listaRetornada);
            Assert.True(listaRetornada.Count == listaPacientes.Count);
        }
    }
}
