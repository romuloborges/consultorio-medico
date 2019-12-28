using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsultorioMedico.Application.Service
{
    public class PacienteService : IPacienteService
    {
        private IPacienteRepository pacienteRepository;
        public PacienteService(IPacienteRepository pacienteRepository)
        {
            this.pacienteRepository = pacienteRepository;
        }

        public PacienteAgendarConsultaViewModel ObterPacienteConsulta(string id)
        {
            var p = this.pacienteRepository.BuscarPacientePorId(new Guid(id));

            return new PacienteAgendarConsultaViewModel(p.IdPaciente.ToString(), p.Nome, p.DataNascimento, p.Cpf, new EnderecoViewModel(p.Endereco.Cep, p.Endereco.Logradouro, p.Endereco.Numero, p.Endereco.Complemento, p.Endereco.Bairro, p.Endereco.Localidade, p.Endereco.Uf));
        }

        public IEnumerable<PacienteMatSelect> ObterTodosPacientesParaMatSelect()
        {
            var listaPacientes = this.pacienteRepository.ObterTodosPacientesSemEndereco();
            var listaPacientesSelect = new List<PacienteMatSelect>();

            foreach(Paciente p in listaPacientes)
            {
                listaPacientesSelect.Add(new PacienteMatSelect(p.IdPaciente.ToString(), p.Nome));
            }

            return listaPacientesSelect.OrderBy(paciente => paciente.Nome);
        }
        //public IEnumerable<PacienteAgendarConsultaViewModel> ObterTodosPacientes()
        //{
        //    var listaPacientes = this.pacienteRepository.ObterTodosPacientes();
        //    var listaPacientesAgendarConsultaViewModel = new List<PacienteAgendarConsultaViewModel>();

        //    foreach(Paciente p in listaPacientes)
        //    {
        //        listaPacientesAgendarConsultaViewModel.Add(new PacienteAgendarConsultaViewModel(p.IdPaciente.ToString(), p.Nome, p.DataNascimento, p.Cpf, new EnderecoViewModel(p.Endereco.Cep, p.Endereco.Logradouro, p.Endereco.Numero, p.Endereco.Complemento, p.Endereco.Bairro, p.Endereco.Localidade, p.Endereco.Uf)));
        //    }

        //    return listaPacientesAgendarConsultaViewModel;
        //}
    }
}
