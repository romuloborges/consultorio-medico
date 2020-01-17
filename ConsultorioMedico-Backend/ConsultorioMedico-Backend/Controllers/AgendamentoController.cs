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
        public async Task<Mensagem> CadastrarAgendamento([FromBody] AgendamentoCadastrarViewModel agendamentoViewModel)
        {
            return await this.agendamentoService.CadastrarAgendamento(agendamentoViewModel);
        }

        [Route("atualizar")]
        [HttpPut]
        public async Task<Mensagem> AtualizarAgendamento([FromBody] AgendamentoComIdViewModel agendamentoComIdViewModel)
        {
            return await this.agendamentoService.AtualizarAgendamento(agendamentoComIdViewModel);
        }

        [Route("obterAgendamentosDataAgendada")]
        [HttpGet]
        public async Task<IEnumerable<AgendamentoListarViewModel>> Get([FromQuery] DateTime dataAgendada, [FromQuery] string id)
        {
            return await this.agendamentoService.BuscarAgendamentoPorDataAgendadaComIdMedico(dataAgendada, id);
        }

        [HttpGet]
        public async Task<IEnumerable<AgendamentoListarViewModel>> Get([FromQuery] DateTime dataHoraInicio, [FromQuery] DateTime dataHoraFim, [FromQuery] string idPaciente, [FromQuery] string idMedico)
        {
            return await this.agendamentoService.BuscarAgendamentoComFiltro(dataHoraInicio, dataHoraFim, idPaciente, idMedico);
        }


        [HttpDelete("{idAgendamento}")]
        public async Task<Mensagem> DeletarAgendamento(string idAgendamento)
        {
            return await this.agendamentoService.DeletarAgendamento(idAgendamento);
        }
    }
}
