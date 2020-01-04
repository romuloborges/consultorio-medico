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
            var pacienteRetorno = new Paciente(Guid.NewGuid(), "José", "", DateTime.Now, "", "", "", "", "", Guid.NewGuid());
            pacienteRetorno.Endereco = new Endereco();
            this.pacienteRepositoryMock.Setup(x => x.BuscarPacientePorId(It.IsAny<Guid>())).Returns(pacienteRetorno);
            var pacienteService = new PacienteService(pacienteRepositoryMock.Object, this.enderecoRepositoryMock.Object, this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var paciente = pacienteService.ObterPacienteConsulta(Guid.NewGuid().ToString());

            //then
            Assert.NotNull(paciente);
            Assert.True(paciente.NomePaciente == pacienteRetorno.Nome);
        }
    }
}
