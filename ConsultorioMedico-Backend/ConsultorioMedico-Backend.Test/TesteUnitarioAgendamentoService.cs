using ConsultorioMedico.Application.Service;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConsultorioMedico_Backend.Test
{
    public class TesteUnitarioAgendamentoService
    {
        private readonly Mock<IAgendamentoRepository> agendamentoRepositoryMock;
        private readonly Mock<IConsultaRepository> consultaRepositoryMock;
        public TesteUnitarioAgendamentoService()
        {
            this.agendamentoRepositoryMock = new Mock<IAgendamentoRepository>();
            this.consultaRepositoryMock = new Mock<IConsultaRepository>();
        }

        [Fact]
        public void CadastrarAgendamentoTest()
        {
            // given
            var agendamento = new AgendamentoCadastrarViewModel(DateTime.Now, DateTime.Now, "Nada", "C62ACB1E-94E1-487F-0F68-08D79090D2CB", "16E16A8D-469F-4286-A470-08D78CC0F920");

            this.agendamentoRepositoryMock.Setup(a => a.BuscarAgendamentoEntreDataEHora(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(new List<Agendamento>());
            this.agendamentoRepositoryMock.Setup(a => a.CadastrarAgendamento(It.IsAny<Agendamento>())).Returns(true);

            var agendamentoService = new AgendamentoService(this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = agendamentoService.CadastrarAgendamento(agendamento);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoCadastrarAgendamentoDentroIntervaloTempoMesmoMedicoTest()
        {
            // given
            var agendamento = new AgendamentoCadastrarViewModel(DateTime.Now, DateTime.Now, "Nada", "C62ACB1E-94E1-487F-0F68-08D79090D2CB", "16E16A8D-469F-4286-A470-08D78CC0F920");
            var lista = new List<Agendamento>();
            lista.Add(new Agendamento());

            this.agendamentoRepositoryMock.Setup(a => a.BuscarAgendamentoEntreDataEHora(agendamento.DataHoraAgendamento.Subtract(new TimeSpan(0, 14, 0)), agendamento.DataHoraAgendamento.Add(new TimeSpan(0, 14, 0)), It.IsAny<Guid>(), new Guid("C62ACB1E-94E1-487F-0F68-08D79090D2CB"))).Returns(lista);
            this.agendamentoRepositoryMock.Setup(a => a.CadastrarAgendamento(It.IsAny<Agendamento>())).Returns(true);

            var agendamentoService = new AgendamentoService(this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = agendamentoService.CadastrarAgendamento(agendamento);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoCadastrarAgendamentoDentroIntervaloMesmoPacienteTempoTest()
        {
            // given
            var agendamento = new AgendamentoCadastrarViewModel(DateTime.Now, DateTime.Now, "Nada", "C62ACB1E-94E1-487F-0F68-08D79090D2CB", "16E16A8D-469F-4286-A470-08D78CC0F920");
            var lista = new List<Agendamento>();
            lista.Add(new Agendamento());

            this.agendamentoRepositoryMock.Setup(a => a.BuscarAgendamentoEntreDataEHora(agendamento.DataHoraAgendamento.Subtract(new TimeSpan(0, 14, 0)), agendamento.DataHoraAgendamento.Add(new TimeSpan(0, 14, 0)), new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), It.IsAny<Guid>())).Returns(lista);
            this.agendamentoRepositoryMock.Setup(a => a.CadastrarAgendamento(It.IsAny<Agendamento>())).Returns(true);

            var agendamentoService = new AgendamentoService(this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = agendamentoService.CadastrarAgendamento(agendamento);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }
        [Fact]
        public void AtualizarAgendamentoTest()
        {
            // given
            var agendamento = new AgendamentoComIdViewModel("418A3CF2-A78F-4AD2-84C6-712638AD048B", DateTime.Now, DateTime.Now, "Nada", "C62ACB1E-94E1-487F-0F68-08D79090D2CB", "16E16A8D-469F-4286-A470-08D78CC0F920");

            this.agendamentoRepositoryMock.Setup(a => a.BuscarAgendamentoEntreDataEHora(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(new List<Agendamento>());
            this.agendamentoRepositoryMock.Setup(a => a.AtualizarAgendamento(It.IsAny<Agendamento>())).Returns(true);

            var agendamentoService = new AgendamentoService(this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = agendamentoService.AtualizarAgendamento(agendamento);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoAtualizarAgendamentoDentroIntervaloTempoMesmoMedicoTest()
        {
            // given
            var agendamento = new AgendamentoComIdViewModel("418A3CF2-A78F-4AD2-84C6-712638AD048B", DateTime.Now, DateTime.Now, "Nada", "C62ACB1E-94E1-487F-0F68-08D79090D2CB", "16E16A8D-469F-4286-A470-08D78CC0F920");
            var lista = new List<Agendamento>();
            lista.Add(new Agendamento());

            this.agendamentoRepositoryMock.Setup(a => a.BuscarAgendamentoEntreDataEHora(agendamento.DataHoraAgendamento.Subtract(new TimeSpan(0, 14, 0)), agendamento.DataHoraAgendamento.Add(new TimeSpan(0, 14, 0)), It.IsAny<Guid>(), new Guid("C62ACB1E-94E1-487F-0F68-08D79090D2CB"))).Returns(lista);
            this.agendamentoRepositoryMock.Setup(a => a.AtualizarAgendamento(It.IsAny<Agendamento>())).Returns(true);

            var agendamentoService = new AgendamentoService(this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = agendamentoService.AtualizarAgendamento(agendamento);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void NaoAtualizarAgendamentoDentroIntervaloMesmoPacienteTempoTest()
        {
            // given
            var agendamento = new AgendamentoComIdViewModel("418A3CF2-A78F-4AD2-84C6-712638AD048B", DateTime.Now, DateTime.Now, "Nada", "C62ACB1E-94E1-487F-0F68-08D79090D2CB", "16E16A8D-469F-4286-A470-08D78CC0F920");
            var lista = new List<Agendamento>();
            lista.Add(new Agendamento());

            this.agendamentoRepositoryMock.Setup(a => a.BuscarAgendamentoEntreDataEHora(agendamento.DataHoraAgendamento.Subtract(new TimeSpan(0, 14, 0)), agendamento.DataHoraAgendamento.Add(new TimeSpan(0, 14, 0)), new Guid("16E16A8D-469F-4286-A470-08D78CC0F920"), It.IsAny<Guid>())).Returns(lista);
            this.agendamentoRepositoryMock.Setup(a => a.AtualizarAgendamento(It.IsAny<Agendamento>())).Returns(true);

            var agendamentoService = new AgendamentoService(this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = agendamentoService.AtualizarAgendamento(agendamento);

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void DeletarAgendamentoSemConsultaTest()
        {
            // given
            this.consultaRepositoryMock.Setup(c => c.BuscarConsultaPorIdAgendamento(It.IsAny<Guid>())).Returns((Consulta) null);
            this.agendamentoRepositoryMock.Setup(a => a.BuscarAgendamentoPorId(It.IsAny<Guid>())).Returns(new Agendamento());
            this.agendamentoRepositoryMock.Setup(a => a.DeletarAgendamento(It.IsAny<Agendamento>())).Returns(true);

            var agendamentoService = new AgendamentoService(this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = agendamentoService.DeletarAgendamento(Guid.NewGuid().ToString());

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 1);
        }

        [Fact]
        public void NaoDeletarAgendamentoComConsultaTest()
        {
            // given
            this.consultaRepositoryMock.Setup(c => c.BuscarConsultaPorIdAgendamento(It.IsAny<Guid>())).Returns(new Consulta());
            this.agendamentoRepositoryMock.Setup(a => a.BuscarAgendamentoPorId(It.IsAny<Guid>())).Returns(new Agendamento());
            this.agendamentoRepositoryMock.Setup(a => a.DeletarAgendamento(It.IsAny<Agendamento>())).Returns(true);

            var agendamentoService = new AgendamentoService(this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var resultado = agendamentoService.DeletarAgendamento(Guid.NewGuid().ToString());

            // then
            Assert.NotNull(resultado);
            Assert.True(resultado.Id == 0);
        }

        [Fact]
        public void BuscarAgendamentoComFiltroTest()
        {
            //given
            Paciente paciente1 = new Paciente(Guid.NewGuid(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", Guid.NewGuid());
            Medico medico1 = new Medico(Guid.NewGuid(), "Marcos", "123.456.789-12", "12.345.678-1", 1214567, new DateTime(1980, 3, 6), "M", "(34)98543-3241", "marcos@email.com", Guid.NewGuid());
            Paciente paciente2 = new Paciente(Guid.NewGuid(), "Joice", "", DateTime.Now, "F", "121.456.789-12", "15.123.456-1", "(21)98767-5433", "joice@email.com", Guid.NewGuid());
            Medico medico2 = new Medico(Guid.NewGuid(), "Joana", "125.456.719-12", "11.345.678-9", 1233567, new DateTime(1980, 9, 1), "F", "(35)91543-3241", "joana@email.com", Guid.NewGuid());

            Agendamento agendamento1 = new Agendamento(Guid.NewGuid(), DateTime.Now, DateTime.Now, "Nada", medico1, paciente1, null);
            Agendamento agendamento2 = new Agendamento(Guid.NewGuid(), DateTime.Now, DateTime.Now, "Nenhuma", medico2, paciente2, null);


            var listaAgendamentos = new List<Agendamento>();
            listaAgendamentos.Add(agendamento1);
            listaAgendamentos.Add(agendamento2);

            this.agendamentoRepositoryMock.Setup(a => a.BuscarAgendamentoSemConsultaComFiltro(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(listaAgendamentos);

            var agendamentoService = new AgendamentoService(this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var listaAgendamentosRetorno = new List<AgendamentoListarViewModel>(agendamentoService.BuscarAgendamentoComFiltro(DateTime.Now, DateTime.Now, "naoha", "naoha"));

            // then
            Assert.NotNull(listaAgendamentosRetorno);
            Assert.True(listaAgendamentosRetorno.Count == listaAgendamentos.Count);
        }

        [Fact]
        public void BuscarAgendamentoPorDataAgendadaComIdMedico()
        {
            // given
            Paciente paciente1 = new Paciente(Guid.NewGuid(), "Joao", "", DateTime.Now, "M", "123.456.789-12", "12.123.456-1", "(21)98764-5433", "joao@email.com", Guid.NewGuid());
            Paciente paciente2 = new Paciente(Guid.NewGuid(), "Joice", "", DateTime.Now, "F", "121.456.789-12", "15.123.456-1", "(21)98767-5433", "joice@email.com", Guid.NewGuid());
            Medico medico1 = new Medico(Guid.NewGuid(), "Marcos", "123.456.789-12", "12.345.678-1", 1214567, new DateTime(1980, 3, 6), "M", "(34)98543-3241", "marcos@email.com", Guid.NewGuid());

            Agendamento agendamento1 = new Agendamento(Guid.NewGuid(), DateTime.Now, DateTime.Now, "Nada", medico1, paciente1, null);
            Agendamento agendamento2 = new Agendamento(Guid.NewGuid(), DateTime.Now, DateTime.Now, "Nenhuma", medico1, paciente2, null);


            var listaAgendamentos = new List<Agendamento>();
            listaAgendamentos.Add(agendamento1);
            listaAgendamentos.Add(agendamento2);

            this.agendamentoRepositoryMock.Setup(a => a.BuscarAgendamentoPorDataAgendadaComIdMedico(It.IsAny<DateTime>(), medico1.IdMedico)).Returns(listaAgendamentos);

            var agendamentoService = new AgendamentoService(this.agendamentoRepositoryMock.Object, this.consultaRepositoryMock.Object);

            // when
            var listaAgendamentosRetorno = new List<AgendamentoListarViewModel>(agendamentoService.BuscarAgendamentoPorDataAgendadaComIdMedico(DateTime.Now, medico1.IdMedico.ToString()));

            // then
            Assert.NotNull(listaAgendamentosRetorno);
            Assert.True(listaAgendamentosRetorno.Count == listaAgendamentos.Count);
        }
    }
}
