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
        public async Task<Mensagem> CadastrarPaciente([FromBody] PacienteCadastrarViewModel pacienteCadastrarViewModel)
        {
            return await this.pacienteService.CadastrarPaciente(pacienteCadastrarViewModel);
        }

        [HttpPut]
        public async Task<Mensagem> AtualizarPaciente([FromBody] PacienteListarEditarViewModel pacienteListarEditarViewModel)
        {
            return await this.pacienteService.AtualizarPaciente(pacienteListarEditarViewModel);
        }

        [HttpGet("{id}")]
        public async Task<PacienteAgendarConsultaViewModel> ObterPacienteConsulta(string id)
        {
            return await this.pacienteService.ObterPacienteConsulta(id);
        }

        [Route("pacienteParaRegistrarConsulta")]
        [HttpGet]
        public async Task<PacienteCadastrarViewModel> ObterPacienteParaRegistrarConsulta([FromQuery] string id)
        {
            return await this.pacienteService.ObterPacienteParaRegistrarConsulta(id);
        }

        [HttpGet]
        public async Task<IEnumerable<PacienteMatSelect>> ObterTodosPacientesListagem()
        {
            return await this.pacienteService.ObterTodosPacientesParaMatSelect();
        }

        [Route("pacientesCompletos")]
        [HttpGet]
        public async Task<IEnumerable<PacienteTabelaListarViewModel>> ObterTodosPacientes()
        {
            return await this.pacienteService.ObterTodosPacientesParaTabela();
        }

        [Route("pacientesComFiltro")]
        [HttpGet]
        public async Task<IEnumerable<PacienteTabelaListarViewModel>> ObterPacientesComFiltroParaTabela([FromQuery] string nome, string cpf, DateTime dataInicio, DateTime dataFim)
        {
            return await this.pacienteService.ObterPacientesComFiltroParaTabela(nome, cpf, dataInicio, dataFim);
        }

        [Route("obterPacienteCompleto")]
        [HttpGet]
        public async Task<PacienteListarEditarViewModel> ObterPacienteCompleto([FromQuery] string id)
        {
            return await this.pacienteService.ObterPacienteCompleto(id);
        }

        [HttpDelete]
        public async Task<Mensagem> DeletarPaciente([FromQuery] string idPaciente)
        {
            return await this.pacienteService.DeletarPaciente(idPaciente);
        }
    }
}
