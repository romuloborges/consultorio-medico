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
        private IAgendamentoRepository agendamentoRepository;
        private IConsultaRepository consultaRepository;
        public PacienteService(IPacienteRepository pacienteRepository, IEnderecoRepository enderecoRepository, IAgendamentoRepository agendamentoRepository, IConsultaRepository consultaRepository)
        {
            this.pacienteRepository = pacienteRepository;
            this.enderecoRepository = enderecoRepository;
            this.agendamentoRepository = agendamentoRepository;
            this.consultaRepository = consultaRepository;
        }

        public Mensagem AtualizarPaciente(PacienteListarEditarViewModel pacienteListarEditarViewModel)
        {
            bool resultado = true;
            Endereco endereco = new Endereco(pacienteListarEditarViewModel.Endereco.Cep, pacienteListarEditarViewModel.Endereco.Logradouro, pacienteListarEditarViewModel.Endereco.Numero, pacienteListarEditarViewModel.Endereco.Complemento, pacienteListarEditarViewModel.Endereco.Bairro, pacienteListarEditarViewModel.Endereco.Localidade, pacienteListarEditarViewModel.Endereco.Uf);
            Guid id = this.enderecoRepository.BuscaIdEndereco(endereco);

            if (id == Guid.Empty)
            {
                // Se não existe este endereço cadastrado, verifica se alguma entidade depende do endereço atual do paciente. Caso positivo, um novo endereço é cadastrado. 
                // Caso contrário, o endereço novo é atualizado sobre o endereço antigo

                int quantidade = this.enderecoRepository.QuantidadeReferenciasEndereco(new Guid(pacienteListarEditarViewModel.Endereco.Id));

                if(quantidade > 1)
                {
                    resultado = this.enderecoRepository.CadastrarEndereco(endereco);
                    id = this.enderecoRepository.BuscaIdEndereco(endereco);
                } else
                {
                    endereco.IdEndereco = new Guid(pacienteListarEditarViewModel.Endereco.Id);
                    resultado = this.enderecoRepository.AtualizarEndereco(endereco);
                    id = endereco.IdEndereco;
                }
            }

            if (!resultado)
            {
                return new Mensagem(0, "Falha ao atualizar paciente!");
            }

            Paciente paciente = new Paciente(new Guid(pacienteListarEditarViewModel.Id), pacienteListarEditarViewModel.Nome, pacienteListarEditarViewModel.NomeSocial, pacienteListarEditarViewModel.DataNascimento, pacienteListarEditarViewModel.Sexo, pacienteListarEditarViewModel.Cpf, pacienteListarEditarViewModel.Rg, pacienteListarEditarViewModel.Telefone, pacienteListarEditarViewModel.Email, id);

            resultado = this.pacienteRepository.AtualizarPaciente(paciente);

            if (!resultado)
            {
                return new Mensagem(0, "Falha ao atualizar paciente!");
            }
            return new Mensagem(1, "Paciente atualizado com sucesso!");
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

        public Mensagem DeletarPaciente(string id)
        {
            Paciente paciente = this.pacienteRepository.BuscarPacientePorId(new Guid(id));

            if(paciente == null)
            {
                return new Mensagem(0, "Este paciente não existe!");
            }

            bool resultado = this.pacienteRepository.DeletarPaciente(paciente);

            if(!resultado)
            {
                return new Mensagem(0, "Falha ao excluir paciente!");
            }

            return new Mensagem(1, "Paciente excluído com sucesso!");
        }

        public PacienteAgendarConsultaViewModel ObterPacienteConsulta(string id)
        {
            var p = this.pacienteRepository.BuscarPacientePorId(new Guid(id));

            return new PacienteAgendarConsultaViewModel(p.IdPaciente.ToString(), p.Nome, p.DataNascimento, p.Cpf, new EnderecoViewModel(p.Endereco.Cep, p.Endereco.Logradouro, p.Endereco.Numero, p.Endereco.Complemento, p.Endereco.Bairro, p.Endereco.Localidade, p.Endereco.Uf));
        }

        public IEnumerable<PacienteTabelaListarViewModel> ObterTodosPacientes()
        {
            var lista = this.pacienteRepository.ObterTodosPacientesComEndereco();
            var listaPacientes = new List<PacienteTabelaListarViewModel>();

            foreach(Paciente p in lista)
            {
                int quantidadeConsultas = this.consultaRepository.ContaConsultasPorPaciente(p.IdPaciente);
                int quantidadeAgendamentos = this.agendamentoRepository.ContarAgendamentosPaciente(p.IdPaciente);
                listaPacientes.Add(new PacienteTabelaListarViewModel(p.IdPaciente.ToString(), p.Nome, p.Cpf, p.Telefone, p.Email, p.DataNascimento, p.Endereco.Localidade, quantidadeConsultas, quantidadeAgendamentos - quantidadeConsultas));
            }

            return listaPacientes;
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
