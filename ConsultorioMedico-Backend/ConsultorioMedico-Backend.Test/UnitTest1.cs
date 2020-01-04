using ConsultorioMedico.Application;
using ConsultorioMedico.Application.Service;
using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
using ConsultorioMedico_Backend.Controllers;
using Moq;
using System;
using Xunit;

namespace ConsultorioMedico_Backend.Test
{
    public class UnitTest1
    {
        private readonly Mock<IPacienteRepository> pacienteRepositoryMock;
        private readonly Mock<IEnderecoRepository> enderecoRepositoryMock;
        private readonly Mock<IAgendamentoRepository> agendamentoRepositoryMock;
        private readonly Mock<IConsultaRepository> consultaRepositoryMock;

        public UnitTest1()
        {
            this.pacienteRepositoryMock = new Mock<IPacienteRepository>();
            this.enderecoRepositoryMock = new Mock<IEnderecoRepository>();
            this.agendamentoRepositoryMock = new Mock<IAgendamentoRepository>();
            this.consultaRepositoryMock = new Mock<IConsultaRepository>();
        }

        [Fact]
        public void ObterPacienteConsultaTest()
        {
            // given            
            var pacienteRetorno = new Paciente(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), "José", "", DateTime.Now, "", "123.456.789-99", "", "", "", Guid.NewGuid());
            var pacienteRetorno2 = new Paciente(new Guid("1E77E2B2-75D8-494C-A471-08D78CC0F920"), "José", "", DateTime.Now, "", "123.456.789-99", "", "", "", Guid.NewGuid());
            pacienteRetorno.Endereco = new Endereco();
            pacienteRetorno2.Endereco = new Endereco();

            this.pacienteRepositoryMock.Setup(x => x.BuscarPacientePorId(new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"))).Returns(pacienteRetorno);
            this.pacienteRepositoryMock.Setup(x => x.BuscarPacientePorId(new Guid("1E77E2B2-75D8-494C-A471-08D78CC0F920"))).Returns(pacienteRetorno2);
            
            var pacienteService = new PacienteService(pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var paciente = pacienteService.ObterPacienteConsulta(Guid.NewGuid().ToString());

            //then
            Assert.NotNull(paciente);
            Assert.True(paciente.NomePaciente == pacienteRetorno.Nome);
        }
    }
}
