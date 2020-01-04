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
        public Mensagem CadastrarConsulta([FromBody] ConsultaCadastrarViewModel consultaCadastrarViewModel)
        {
            return this.consultaService.CadastrarConsulta(consultaCadastrarViewModel);
        }

        [Route("obterTodasConsultasCompletas")]
        [HttpGet]
        public IEnumerable<ConsultaListarViewModel> ObterTodasConsultaCompletas()
        {
            return this.consultaService.ObterTodasConsultasCompletas();
        }
    }
}
