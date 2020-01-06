using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel.Paciente
{
    public class PacienteCadastrarViewModel
    {
        public string Nome { get; set; }
        public string NomeSocial { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public EnderecoViewModel Endereco { get; set; }

        public PacienteCadastrarViewModel()
        {

        }

        public PacienteCadastrarViewModel(string nome, string nomeSocial, DateTime dataNascimento, string sexo, string cpf, string rg, string telefone, string email, EnderecoViewModel endereco)
        {
            this.Nome = nome;
            this.NomeSocial = nomeSocial;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Cpf = cpf;
            this.Rg = rg;
            this.Telefone = telefone;
            this.Email = email;
            this.Endereco = endereco;
        }
    }
}
