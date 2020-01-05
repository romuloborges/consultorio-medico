using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Endereco;
using ConsultorioMedico.Application.ViewModel.Paciente;
using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsultorioMedico.Application.Service
{
    public class PacienteService : IPacienteService
    {
        private IPacienteRepository pacienteRepository;
        private IEnderecoRepository enderecoRepository;
        private IAgendamentoRepository agendamentoRepository;
        private IConsultaRepository consultaRepository;

        private readonly string cpfSemMascara = "^[0-9]{11}$";
        private readonly string cpfComMascara = "^[0-9]{3}\\.[0-9]{3}\\.[0-9]{3}-[0-9]{2}";

        private readonly string rgSemMascara = "^[0-9]{8}([0-9]|[A-Z]{2})$";
        private readonly string rgComMascara = "^[0-9]{2}\\.[0-9]{3}\\.[0-9]{3}-([0-9]|[A-Z]{2})$";

        private readonly string celularSemMascara = "^[0-9]{11}$";
        private readonly string celularComMascara = "^\\([0-9]{2}\\)[0-9]{5}-[0-9]{4}$";

        private readonly string cepSemMascara = "^[0-9]{8}$";
        private readonly string cepComMascara = "^[0-9]{5}-[0-9]{3}$";

        public PacienteService(IPacienteRepository pacienteRepository, IEnderecoRepository enderecoRepository, IAgendamentoRepository agendamentoRepository, IConsultaRepository consultaRepository)
        {
            this.pacienteRepository = pacienteRepository;
            this.enderecoRepository = enderecoRepository;
            this.agendamentoRepository = agendamentoRepository;
            this.consultaRepository = consultaRepository;
        }

        public Mensagem AtualizarPaciente(PacienteListarEditarViewModel pacienteListarEditarViewModel)
        {
            if(!Regex.IsMatch(pacienteListarEditarViewModel.Cpf, cpfComMascara))
            {
                if(Regex.IsMatch(pacienteListarEditarViewModel.Cpf, cpfSemMascara))
                {
                    pacienteListarEditarViewModel.Cpf = pacienteListarEditarViewModel.Cpf.Substring(0, 3) + "." + pacienteListarEditarViewModel.Cpf.Substring(3, 3) + "." + pacienteListarEditarViewModel.Cpf.Substring(6, 3) + "-" + pacienteListarEditarViewModel.Cpf.Substring(9, 2);
                } else
                {
                    return new Mensagem(0, "CPF não possui o formato correto!");
                }
            }

            if(!Regex.IsMatch(pacienteListarEditarViewModel.Rg, rgComMascara))
            {
                if(Regex.IsMatch(pacienteListarEditarViewModel.Rg, rgSemMascara))
                {
                    pacienteListarEditarViewModel.Rg = pacienteListarEditarViewModel.Rg.Substring(0, 2) + "." + pacienteListarEditarViewModel.Rg.Substring(2, 3) + "." + pacienteListarEditarViewModel.Rg.Substring(5, 3) + "-" + pacienteListarEditarViewModel.Rg.Substring(8);
                } else
                {
                    return new Mensagem(0, "RG não possui o formato correto!");
                }
            }

            if(!Regex.IsMatch(pacienteListarEditarViewModel.Telefone, celularComMascara))
            {
                if(Regex.IsMatch(pacienteListarEditarViewModel.Telefone, celularSemMascara))
                {
                    pacienteListarEditarViewModel.Telefone = "(" + pacienteListarEditarViewModel.Telefone.Substring(0, 2) + ")" + pacienteListarEditarViewModel.Telefone.Substring(2, 5) + "-" + pacienteListarEditarViewModel.Telefone.Substring(7);
                } else
                {
                    return new Mensagem(0, "RG não possui o formato correto!");
                }
            }

            if (!Regex.IsMatch(pacienteListarEditarViewModel.Endereco.Cep, cepComMascara))
            {
                if (Regex.IsMatch(pacienteListarEditarViewModel.Endereco.Cep, cepSemMascara))
                {
                    pacienteListarEditarViewModel.Endereco.Cep = pacienteListarEditarViewModel.Endereco.Cep.Substring(0, 5) + "-" + pacienteListarEditarViewModel.Endereco.Cep.Substring(5);
                }
                else
                {
                    return new Mensagem(0, "RG não possui o formato correto!");
                }
            }

            

            if(this.pacienteRepository.BuscarPacientePorCpf(pacienteListarEditarViewModel.Cpf) != null)
            {
                return new Mensagem(0, "Já existe um paciente cadastrado com este CPF!");
            }

            if (this.pacienteRepository.BuscarPacientePorRg(pacienteListarEditarViewModel.Rg) != null)
            {
                return new Mensagem(0, "Já existe um paciente cadastrado com esse RG!");
            }

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
            if (!Regex.IsMatch(pacienteCadastrarViewModel.Cpf, cpfComMascara))
            {
                if (Regex.IsMatch(pacienteCadastrarViewModel.Cpf, cpfSemMascara))
                {
                    pacienteCadastrarViewModel.Cpf = pacienteCadastrarViewModel.Cpf.Substring(0, 3) + "." + pacienteCadastrarViewModel.Cpf.Substring(3, 3) + "." + pacienteCadastrarViewModel.Cpf.Substring(6, 3) + "-" + pacienteCadastrarViewModel.Cpf.Substring(9, 2);
                }
                else
                {
                    return new Mensagem(0, "CPF não possui o formato correto!");
                }
            }

            if (!Regex.IsMatch(pacienteCadastrarViewModel.Rg, rgComMascara))
            {
                if (Regex.IsMatch(pacienteCadastrarViewModel.Rg, rgSemMascara))
                {
                    pacienteCadastrarViewModel.Rg = pacienteCadastrarViewModel.Rg.Substring(0, 2) + "." + pacienteCadastrarViewModel.Rg.Substring(2, 3) + "." + pacienteCadastrarViewModel.Rg.Substring(5, 3) + "-" + pacienteCadastrarViewModel.Rg.Substring(8);
                }
                else
                {
                    return new Mensagem(0, "RG não possui o formato correto!");
                }
            }

            if (!Regex.IsMatch(pacienteCadastrarViewModel.Telefone, celularComMascara))
            {
                if (Regex.IsMatch(pacienteCadastrarViewModel.Telefone, celularSemMascara))
                {
                    pacienteCadastrarViewModel.Telefone = "(" + pacienteCadastrarViewModel.Telefone.Substring(0, 2) + ")" + pacienteCadastrarViewModel.Telefone.Substring(2, 5) + "-" + pacienteCadastrarViewModel.Telefone.Substring(7);
                }
                else
                {
                    return new Mensagem(0, "RG não possui o formato correto!");
                }
            }

            if (!Regex.IsMatch(pacienteCadastrarViewModel.Endereco.Cep, cepComMascara))
            {
                if (Regex.IsMatch(pacienteCadastrarViewModel.Endereco.Cep, cepSemMascara))
                {
                    pacienteCadastrarViewModel.Endereco.Cep = pacienteCadastrarViewModel.Endereco.Cep.Substring(0, 5) + "-" + pacienteCadastrarViewModel.Endereco.Cep.Substring(5);
                }
                else
                {
                    return new Mensagem(0, "RG não possui o formato correto!");
                }
            }

            if (this.pacienteRepository.BuscarPacientePorCpf(pacienteCadastrarViewModel.Cpf) != null)
            {
                return new Mensagem(0, "Já existe um paciente cadastrado com este CPF!");
            }

            if (this.pacienteRepository.BuscarPacientePorRg(pacienteCadastrarViewModel.Rg) != null)
            {
                return new Mensagem(0, "Já existe um paciente cadastrado com esse RG!");
            }

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

            this.agendamentoRepository.ContarAgendamentosPaciente(paciente.IdPaciente);

            bool resultado = this.pacienteRepository.DeletarPaciente(paciente);

            if(!resultado)
            {
                return new Mensagem(0, "Falha ao excluir paciente!");
            }

            return new Mensagem(1, "Paciente excluído com sucesso!");
        }

        public PacienteListarEditarViewModel ObterPacienteCompleto(string id)
        {
            var paciente = this.pacienteRepository.BuscarPacientePorId(new Guid(id));

            return new PacienteListarEditarViewModel(paciente.IdPaciente.ToString(), paciente.Nome, paciente.NomeSocial, paciente.DataNascimento, paciente.Sexo, paciente.Cpf, paciente.Rg, paciente.Telefone, paciente.Email, new EnderecoListarEditarViewModel(paciente.Endereco.IdEndereco.ToString(), paciente.Endereco.Cep, paciente.Endereco.Logradouro, paciente.Endereco.Numero, paciente.Endereco.Complemento, paciente.Endereco.Bairro, paciente.Endereco.Localidade, paciente.Endereco.Uf));
        }

        public PacienteAgendarConsultaViewModel ObterPacienteConsulta(string id)
        {
            var p = this.pacienteRepository.BuscarPacientePorId(new Guid(id));

            return new PacienteAgendarConsultaViewModel(p.IdPaciente.ToString(), p.Nome, p.DataNascimento, p.Cpf, new EnderecoViewModel(p.Endereco.Cep, p.Endereco.Logradouro, p.Endereco.Numero, p.Endereco.Complemento, p.Endereco.Bairro, p.Endereco.Localidade, p.Endereco.Uf));
        }

        public PacienteCadastrarViewModel ObterPacienteParaRegistrarConsulta(string id)
        {
            var paciente = this.pacienteRepository.BuscarPacientePorId(new Guid(id));

            if(paciente == null)
            {
                return null;
            }

            PacienteCadastrarViewModel p = new PacienteCadastrarViewModel(paciente.Nome, paciente.NomeSocial, paciente.DataNascimento, paciente.Sexo, paciente.Cpf, paciente.Rg, paciente.Telefone, paciente.Email, new EnderecoViewModel(paciente.Endereco.Cep, paciente.Endereco.Logradouro, paciente.Endereco.Numero, paciente.Endereco.Complemento, paciente.Endereco.Bairro, paciente.Endereco.Localidade, paciente.Endereco.Uf));

            return p;
        }

        public IEnumerable<PacienteTabelaListarViewModel> ObterPacientesComFiltro(string nome, string cpf, DateTime dataInicio, DateTime dataFim)
        {
            nome = nome.Equals("naoha") ? "" : nome;
            if(!cpf.Equals("naoha"))
            {
                if(Regex.IsMatch(cpf, cpfSemMascara))
                {
                    cpf = cpf.Substring(0, 3) + "." + cpf.Substring(3, 3) + "." + cpf.Substring(6, 3) + "-" + cpf.Substring(9, 2);
                }
            } else
            {
                cpf = "";
            }

            var lista = this.pacienteRepository.ObterPacientesComFiltro(nome, cpf, dataInicio, dataFim);
            var listaPacientes = new List<PacienteTabelaListarViewModel>();

            foreach (Paciente p in lista)
            {
                int quantidadeConsultas = this.consultaRepository.ContaConsultasPorPaciente(p.IdPaciente);
                int quantidadeAgendamentos = this.agendamentoRepository.ContarAgendamentosPaciente(p.IdPaciente);
                listaPacientes.Add(new PacienteTabelaListarViewModel(p.IdPaciente.ToString(), p.Nome, p.Cpf, p.Telefone, p.Email, p.DataNascimento, p.Endereco.Localidade, quantidadeConsultas, quantidadeAgendamentos - quantidadeConsultas));
            }

            return listaPacientes;
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
