using ConsultorioMedico.Application;
using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Consulta;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultorioMedico_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultaController
    {
        private IConsultaService consultaService;

        public ConsultaController(IConsultaService consultaService)
        {
            this.consultaService = consultaService;
        }

        [HttpPost]
        public async Task<Mensagem> CadastrarConsulta([FromBody] ConsultaCadastrarViewModel consultaCadastrarViewModel)
        {
            return await this.consultaService.CadastrarConsulta(consultaCadastrarViewModel);
        }

        [Route("atualizarConsulta")]
        [HttpPut]
        public async Task<Mensagem> AtualizarConsulta([FromBody] ConsultaComIdAgendamentoViewModel consultaViewModel)
        {
            return await this.consultaService.AtualizarConsulta(consultaViewModel);
        }

        [Route("obterConsultasCompletasComFiltro")]
        [HttpGet]
        public async Task<IEnumerable<ConsultaListarViewModel>> ObterTodasConsultaCompletasComFiltro([FromQuery] DateTime dataHoraTerminoConsulta, [FromQuery] DateTime dataHoraAgendamento, [FromQuery] string idPaciente)
        {
            return await this.consultaService.ObterConsultasCompletasComFiltro(dataHoraTerminoConsulta, dataHoraAgendamento, idPaciente);
        }

        [Route("deletarConsulta")]
        [HttpDelete]
        public async Task<Mensagem> DeletarConsulta([FromQuery] string id)
        {
            return await this.consultaService.DeletarConsulta(id);
        }
    }
}
