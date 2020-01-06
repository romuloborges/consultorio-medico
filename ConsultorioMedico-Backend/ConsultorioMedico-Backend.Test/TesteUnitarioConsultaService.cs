using ConsultorioMedico.Application.Service;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Consulta;
using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConsultorioMedico_Backend.Test
{
    public class TesteUnitarioConsultaService
    {
        private readonly Mock<IConsultaRepository> consultaRepositoryMock;
        public TesteUnitarioConsultaService()
        {
            this.consultaRepositoryMock = new Mock<IConsultaRepository>();
        }

        [Fact]
        public void CadastrarConsultaTest()
        {
            // given
            var consulta = new ConsultaCadastrarViewModel(DateTime.Now, "Dipirona", DateTime.MinValue.AddMinutes(15), Guid.NewGuid().ToString());

            this.consultaRepositoryMock.Setup(c => c.CadastrarConsulta(It.IsAny<Consulta>())).Returns(true);

            var consultaService = new ConsultaService(this.consultaRepositoryMock.Object);

            // when
            var resultado = consultaService.CadastrarConsulta(consulta);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoCadastrarConsultaTest()
        {
            // given
            var consulta = new ConsultaCadastrarViewModel(DateTime.Now, "Dipirona", DateTime.MinValue.AddMinutes(15), Guid.NewGuid().ToString());

            this.consultaRepositoryMock.Setup(c => c.CadastrarConsulta(It.IsAny<Consulta>())).Returns(false);

            var consultaService = new ConsultaService(this.consultaRepositoryMock.Object);

            // when
            var resultado = consultaService.CadastrarConsulta(consulta);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void AtualizarConsultaTest()
        {
            // given
            var consulta = new ConsultaComIdAgendamentoViewModel(Guid.NewGuid().ToString(), DateTime.Now, "Dipirona. Buscopan.", DateTime.MinValue.AddMinutes(15), Guid.NewGuid().ToString());

            this.consultaRepositoryMock.Setup(c => c.AtualizarConsulta(It.IsAny<Consulta>())).Returns(true);

            var consultaService = new ConsultaService(this.consultaRepositoryMock.Object);

            // when
            var resultado = consultaService.AtualizarConsulta(consulta);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoAtualizarConsultaTest()
        {
            // given
            var consulta = new ConsultaComIdAgendamentoViewModel(Guid.NewGuid().ToString(), DateTime.Now, "Dipirona. Buscopan.", DateTime.MinValue.AddMinutes(15), Guid.NewGuid().ToString());

            this.consultaRepositoryMock.Setup(c => c.AtualizarConsulta(It.IsAny<Consulta>())).Returns(false);

            var consultaService = new ConsultaService(this.consultaRepositoryMock.Object);

            // when
            var resultado = consultaService.AtualizarConsulta(consulta);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void DeletarConsultaTest()
        {
            // given
            var consulta = new Consulta(Guid.NewGuid(), DateTime.Now, "Dipirona", DateTime.MinValue.AddMinutes(20), Guid.NewGuid());

            this.consultaRepositoryMock.Setup(c => c.BuscarConsultaPorId(consulta.IdConsulta)).Returns(consulta);
            this.consultaRepositoryMock.Setup(c => c.DeletarConsulta(It.IsAny<Consulta>())).Returns(true);

            var consultaService = new ConsultaService(this.consultaRepositoryMock.Object);

            // when
            var resultado = consultaService.DeletarConsulta(consulta.IdConsulta.ToString());

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoDeletarConsultaTest()
        {
            // given
            var consulta = new Consulta(Guid.NewGuid(), DateTime.Now, "Dipirona", DateTime.MinValue.AddMinutes(20), Guid.NewGuid());

            this.consultaRepositoryMock.Setup(c => c.BuscarConsultaPorId(consulta.IdConsulta)).Returns(consulta);
            this.consultaRepositoryMock.Setup(c => c.DeletarConsulta(It.IsAny<Consulta>())).Returns(false);

            var consultaService = new ConsultaService(this.consultaRepositoryMock.Object);

            // when
            var resultado = consultaService.DeletarConsulta(consulta.IdConsulta.ToString());

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoDeletarConsultaIdInvalidoTest()
        {
            // given
            var consulta = new Consulta(Guid.NewGuid(), DateTime.Now, "Dipirona", DateTime.MinValue.AddMinutes(20), Guid.NewGuid());

            this.consultaRepositoryMock.Setup(c => c.BuscarConsultaPorId(consulta.IdConsulta)).Returns((Consulta) null);
            this.consultaRepositoryMock.Setup(c => c.DeletarConsulta(It.IsAny<Consulta>())).Returns(true);

            var consultaService = new ConsultaService(this.consultaRepositoryMock.Object);

            // when
            var resultado = consultaService.DeletarConsulta(consulta.IdConsulta.ToString());

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void ObterConsultasCompletasComFiltroTest()
        {
            // given
            Paciente paciente1 = new Paciente(Guid.NewGuid(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", Guid.NewGuid());
            Medico medico1 = new Medico(Guid.NewGuid(), "Marcos", "123.456.789-12", "12.345.678-1", 1214567, new DateTime(1980, 3, 6), "M", "(34)98543-3241", "marcos@email.com", Guid.NewGuid());
            Medico medico2 = new Medico(Guid.NewGuid(), "Joana", "125.456.719-12", "11.345.678-9", 1233567, new DateTime(1980, 9, 1), "F", "(35)91543-3241", "joana@email.com", Guid.NewGuid());

            Agendamento agendamento1 = new Agendamento(Guid.NewGuid(), DateTime.Now, DateTime.Now, "Nada", medico1, paciente1, null);
            Agendamento agendamento2 = new Agendamento(Guid.NewGuid(), DateTime.Now, DateTime.Now, "Nenhuma", medico2, paciente1, null);

            Consulta consulta1 = new Consulta(Guid.NewGuid(), DateTime.Now, "Dipirona", DateTime.MinValue.AddMinutes(15), agendamento1);
            Consulta consulta2 = new Consulta(Guid.NewGuid(), DateTime.Now, "Buscopan", DateTime.MinValue.AddMinutes(10), agendamento2);

            var listaConsultas = new List<Consulta>();
            listaConsultas.Add(consulta1);
            listaConsultas.Add(consulta2);

            this.consultaRepositoryMock.Setup(c => c.ObterConsultasCompletasComFiltro(It.IsAny<DateTime>(), It.IsAny<DateTime>(), paciente1.IdPaciente)).Returns(listaConsultas);

            var consultaService = new ConsultaService(this.consultaRepositoryMock.Object);

            // when
            var listaConsultasRetorno = new List<ConsultaListarViewModel>(consultaService.ObterConsultasCompletasComFiltro(DateTime.Now, DateTime.Now, paciente1.IdPaciente.ToString()));

            // then
            Assert.NotNull(listaConsultasRetorno);
            Assert.True(listaConsultas.Count == 2);
        }
    }
}
