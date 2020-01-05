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

        [Route("obterAgendamentosDataAgendada")]
        [HttpGet]
        public IEnumerable<AgendamentoListarViewModel> Get([FromQuery] DateTime dataAgendada, [FromQuery] string id)
        {
            return this.agendamentoService.BuscarAgendamentoPorDataAgendada(dataAgendada, id);
        }

        [HttpGet]
        public IEnumerable<AgendamentoListarViewModel> Get([FromQuery] DateTime dataHoraInicio, [FromQuery] DateTime dataHoraFim, [FromQuery] string idPaciente, [FromQuery] string idMedico)
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
