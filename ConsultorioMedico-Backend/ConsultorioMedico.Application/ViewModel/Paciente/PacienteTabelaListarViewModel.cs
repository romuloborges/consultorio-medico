using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel.Paciente
{
    public class PacienteTabelaListarViewModel
    {
        public string IdPaciente { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Localidade { get; set; }
        public int QuantidadeConsultas { get; set; }
        public int QuantidadeAgendamentosPendentes { get; set; }

        public PacienteTabelaListarViewModel()
        {

        }

        public PacienteTabelaListarViewModel(string idPaciente, string nome, string cpf, string telefone, string email, DateTime dataNascimento, string localidade, int quantidadeConsultas, int quantidadeAgendamentosPendentes)
        {
            this.IdPaciente = idPaciente;
            this.Nome = nome;
            this.Cpf = cpf;
            this.Telefone = telefone;
            this.Email = email;
            this.DataNascimento = dataNascimento;
            this.Localidade = localidade;
            this.QuantidadeConsultas = quantidadeConsultas;
            this.QuantidadeAgendamentosPendentes = quantidadeAgendamentosPendentes;
        }
    }
}
