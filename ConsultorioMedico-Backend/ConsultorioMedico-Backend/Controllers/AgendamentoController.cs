using ConsultorioMedico.Application;
using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultorioMedico_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AgendamentoController
    {
        private IAgendamentoService agendamentoService;

        public AgendamentoController(IAgendamentoService agendamentoService)
        {
            this.agendamentoService = agendamentoService;
        }

        [Route("cadastrar")]
        [HttpPost]
        public Mensagem CadastrarAgendamento([FromBody] AgendamentoViewModel agendamentoViewModel)
        {
            return this.agendamentoService.CadastrarAgendamento(agendamentoViewModel);
        }

        [Route("atualizar")]
        [HttpPut]
        public Mensagem AtualizarAgendamento([FromBody] AgendamentoComIdViewModel agendamentoComIdViewModel)
        {
            return this.agendamentoService.AtualizarAgendamento(agendamentoComIdViewModel);
        }

        [HttpGet("{dataHoraAgendada}")]
        public IEnumerable<AgendamentoListarViewModel> Get(DateTime dataHoraAgendada)
        {
            return this.agendamentoService.BuscarAgendamentoPorDataAgendada(dataHoraAgendada);
        }

<<<<<<< HEAD
        [HttpGet]
        public IEnumerable<AgendamentoListarViewModel> Get([FromQuery] DateTime dataHoraInicio, [FromQuery] DateTime dataHoraFim, [FromQuery] string idPaciente, [FromQuery] string idMedico, [FromQuery] int filtrarNaoConsultados)
        {
            bool aindaNaoConsultados = filtrarNaoConsultados == 1;
            return this.agendamentoService.BuscarAgendamentoComFiltro(dataHoraInicio, dataHoraFim, idPaciente, idMedico, aindaNaoConsultados);
=======
        //[HttpGet("{dataHoraInicio}/{dataHoraFim}/{idPaciente}/{idMedico}")]
        //public IEnumerable<AgendamentoListarViewModel> Get(DateTime dataHoraInicio, DateTime dataHoraFim, string idPaciente, string idMedico)
        //{
        //    return this.agendamentoService.BuscarAgendamentoComFiltro(dataHoraInicio, dataHoraFim, idPaciente, idMedico);
        //}

        [HttpGet]
        public IEnumerable<AgendamentoListarViewModel> Get([FromQuery] DateTime dataHoraInicio, [FromQuery] DateTime dataHoraFim, [FromQuery] string idPaciente, [FromQuery] string idMedico, [FromQuery] bool jaConsultados)
        {
            return this.agendamentoService.BuscarAgendamentoComFiltro(dataHoraInicio, dataHoraFim, idPaciente, idMedico, jaConsultados);
>>>>>>> develop
        }


        [HttpDelete("{idAgendamento}")]
        public Mensagem DeletarAgendamento(string idAgendamento)
        {
            return this.agendamentoService.DeletarAgendamento(idAgendamento);
        }
    }
}
