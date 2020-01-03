using ConsultorioMedico.Application;
using ConsultorioMedico.Application.Service;
using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
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
        private Mock<IAgendamentoService> agendamentoService;
        private Mock<IAgendamentoRepository> agendamentoRepository;
        private Mock<IConsultaRepository> consultaRepository;
        private Mock<ConsultorioMedicoContext> context;

        [Fact]
        public void Test1()
        {
            //agendamentoService = new Mock<IAgendamentoService>();

            //var controller = new AgendamentoController(agendamentoService.Object);

            //AgendamentoViewModel agendamento = new AgendamentoViewModel(DateTime.Now.AddMinutes(10), DateTime.Now, "Sem observações", "5868E87B-C6DD-410E-9388-50AFB53D1BB7", "16E16A8D-469F-4286-A470-08D78CC0F920");

            //Mensagem m = controller.CadastrarAgendamento(agendamento);

            //Assert.True(m.Id == 0);

            //this.agendamentoRepository = new Mock<AgendamentoRepository(context) > ();
            //this.consultaRepository = new Mock<ConsultaRepository>(context);

            //var agendamentoS = new AgendamentoService(this.agendamentoRepository.Object, this.consultaRepository.Object);

            //Mensagem m = agendamentoS.CadastrarAgendamento(agendamento);

            //Assert.True(m.Id == 0);

        }
    }
}
