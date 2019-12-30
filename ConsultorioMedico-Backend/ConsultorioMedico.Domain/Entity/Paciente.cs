using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Domain.Entity
{
    public class Paciente
    {
        public Guid IdPaciente { get; set; }
        public string Nome { get; set; }
        public string? NomeSocial { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public Guid IdEndereco { get; set; }
        public Endereco Endereco { get; set; }
        public List<Agendamento> Agendamentos { get; set; }

        public Paciente()
        {

        }

        public Paciente(string nome, string? nomeSocial, DateTime dataNascimento, string sexo, string cpf, string rg, string telefone, string email, Guid idEndereco)
        {
            this.Nome = nome;
            this.NomeSocial = nomeSocial;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Cpf = cpf;
            this.Rg = rg;
            this.Telefone = telefone;
            this.Email = email;
            this.IdEndereco = idEndereco;
        }

        public Paciente(Guid idPaciente, string nome, string? nomeSocial, DateTime dataNascimento, string sexo, string cpf, string rg, string telefone, string email, Guid idEndereco, Endereco endereco, List<Agendamento> agendamentos)
        {
            this.IdPaciente = idPaciente;
            this.Nome = nome;
            this.NomeSocial = nomeSocial;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Cpf = cpf;
            this.Rg = rg;
            this.Telefone = telefone;
            this.Email = email;
            this.IdEndereco = idEndereco;
            this.Endereco = endereco;
            this.Agendamentos = agendamentos;
        }
    }
}
