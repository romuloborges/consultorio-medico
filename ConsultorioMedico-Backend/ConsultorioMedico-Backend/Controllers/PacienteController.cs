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
    public class PacienteController
    {
        private IPacienteService pacienteService;
        public PacienteController(IPacienteService pacienteService)
        {
            this.pacienteService = pacienteService;
        }

        [HttpGet("{id}")]
        public PacienteAgendarConsultaViewModel ObterPacienteConsulta(string id)
        {
            return this.pacienteService.ObterPacienteConsulta(id);
        }

        [HttpGet]
        public IEnumerable<PacienteMatSelect> ObterTodosPacientesListagem()
        {
            return this.pacienteService.ObterTodosPacientesParaMatSelect();
        }

    }
}
