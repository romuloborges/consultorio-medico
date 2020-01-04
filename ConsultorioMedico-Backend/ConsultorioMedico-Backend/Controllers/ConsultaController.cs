using ConsultorioMedico.Application;
using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
<<<<<<< HEAD
using ConsultorioMedico.Application.ViewModel.Consulta;
=======
>>>>>>> develop
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
        public Mensagem CadastrarConsulta([FromBody] ConsultaCadastrarViewModel consultaCadastrarViewModel)
        {
            return this.consultaService.CadastrarConsulta(consultaCadastrarViewModel);
        }

<<<<<<< HEAD
        [Route("atualizarConsulta")]
        [HttpPut]
        public Mensagem AtualizarConsulta([FromBody] ConsultaComIdAgendamentoViewModel consultaViewModel)
        {
            return this.consultaService.AtualizarConsulta(consultaViewModel);
        }

        [Route("obterConsultasCompletasComFiltro")]
        [HttpGet]
        public IEnumerable<ConsultaListarViewModel> ObterTodasConsultaCompletasComFiltro([FromQuery] DateTime dataHoraTerminoConsulta, [FromQuery] DateTime dataHoraAgendamento, [FromQuery] string idPaciente)
        {
            return this.consultaService.ObterConsultasCompletasComFiltro(dataHoraTerminoConsulta, dataHoraAgendamento, idPaciente);
        }

        [Route("deletarConsulta")]
        [HttpDelete]
        public Mensagem DeletarConsulta([FromQuery] string id)
        {
            return this.consultaService.DeletarConsulta(id);
        }
=======
>>>>>>> develop
    }
}
