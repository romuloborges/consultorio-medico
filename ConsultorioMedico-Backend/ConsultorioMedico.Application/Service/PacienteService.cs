using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Paciente;
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
        private IEnderecoRepository enderecoRepository;
        public PacienteService(IPacienteRepository pacienteRepository, IEnderecoRepository enderecoRepository)
        {
            this.pacienteRepository = pacienteRepository;
            this.enderecoRepository = enderecoRepository;
        }

        public Mensagem CadastrarPaciente(PacienteCadastrarViewModel pacienteCadastrarViewModel)
        {
            bool resultado = true;
            Endereco endereco = new Endereco(pacienteCadastrarViewModel.Endereco.Cep, pacienteCadastrarViewModel.Endereco.Logradouro, pacienteCadastrarViewModel.Endereco.Numero, pacienteCadastrarViewModel.Endereco.Complemento, pacienteCadastrarViewModel.Endereco.Bairro, pacienteCadastrarViewModel.Endereco.Localidade, pacienteCadastrarViewModel.Endereco.Uf);
            Guid id = this.enderecoRepository.BuscaIdEndereco(endereco);

            if(id == Guid.Empty)
            {
                resultado = this.enderecoRepository.CadastrarEndereco(endereco);
                id = this.enderecoRepository.BuscaIdEndereco(endereco);
            }

            if(!resultado)
            {
                return new Mensagem(0, "Falha ao cadastrar paciente!");
            }

            Paciente paciente = new Paciente(pacienteCadastrarViewModel.Nome, pacienteCadastrarViewModel.NomeSocial, pacienteCadastrarViewModel.DataNascimento, pacienteCadastrarViewModel.Sexo, pacienteCadastrarViewModel.Cpf, pacienteCadastrarViewModel.Rg, pacienteCadastrarViewModel.Telefone, pacienteCadastrarViewModel.Email, id);

            resultado = this.pacienteRepository.CadastrarPaciente(paciente);

            if(!resultado)
            {
                return new Mensagem(0, "Falha ao cadastrar paciente!");
            }
            return new Mensagem(1, "Paciente cadastrado com sucesso!");
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
