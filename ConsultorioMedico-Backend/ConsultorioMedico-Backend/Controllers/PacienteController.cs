using ConsultorioMedico.Application;
using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Paciente;
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

        [HttpPost]
        public Mensagem CadastrarPaciente(PacienteCadastrarViewModel pacienteCadastrarViewModel)
        {
            return this.pacienteService.CadastrarPaciente(pacienteCadastrarViewModel);
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
