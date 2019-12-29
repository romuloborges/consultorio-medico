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
        public string AtualizarAgendamento([FromBody] AgendamentoComIdViewModel agendamentoComIdViewModel)
        {
            return this.agendamentoService.AtualizarAgendamento(agendamentoComIdViewModel);
        }

        [HttpGet("{dataHoraAgendada}")]
        public IEnumerable<AgendamentoListarViewModel> Get(DateTime dataHoraAgendada)
        {
            return this.agendamentoService.BuscarAgendamentoPorDataAgendada(dataHoraAgendada);
        }

        [HttpGet("{dataHoraInicio?}/{dataHoraFim?}/{idPaciente?}/{idMedico?}")]
        public IEnumerable<AgendamentoListarViewModel> Get(DateTime? dataHoraInicio, DateTime? dataHoraFim, string? idPaciente, string? idMedico)
        {
            return this.agendamentoService.BuscarAgendamentoComFiltro(dataHoraInicio, dataHoraFim, idPaciente, idMedico);
        }


        [HttpDelete("{idAgendamento}")]
        public Mensagem DeletarAgendamento(string idAgendamento)
        {
            return this.agendamentoService.DeletarAgendamento(idAgendamento);
        }
    }
}
