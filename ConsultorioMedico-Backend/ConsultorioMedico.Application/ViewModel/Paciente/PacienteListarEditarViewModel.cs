using ConsultorioMedico.Application.ViewModel.Endereco;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel.Paciente
{
    public class PacienteListarEditarViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string NomeSocial { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public EnderecoListarEditarViewModel Endereco { get; set; }

        public PacienteListarEditarViewModel()
        {

        }

        public PacienteListarEditarViewModel(string id, string nome, string nomeSocial, DateTime dataNascimento, string sexo, string cpf, string rg, string telefone, string email, EnderecoListarEditarViewModel endereco)
        {
            this.Id = id;
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
