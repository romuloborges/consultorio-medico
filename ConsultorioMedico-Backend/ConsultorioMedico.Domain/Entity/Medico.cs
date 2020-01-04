using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Domain.Entity
{
    public class Medico
    {
        public Guid IdMedico { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public int Crm { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public Guid IdEndereco { get; set; }
        public Endereco Endereco { get; set; }
        public List<Agendamento> Agendamentos { get; set; }
        public Usuario Usuario { get; set; }

        public Medico()
        {

        }

<<<<<<< HEAD
        public Medico(string nome, string cpf, string rg, int crm, DateTime dataNascimento, string sexo, string telefone, string email, Guid idEndereco)
        {
            this.Nome = nome;
            this.Cpf = cpf;
            this.Rg = rg;
            this.Crm = crm;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Telefone = telefone;
            this.Email = email;
            this.IdEndereco = idEndereco;
        }

=======
>>>>>>> develop
        public Medico(Guid idMedico, string nome, string cpf, string rg, int crm, DateTime dataNascimento, string sexo, string telefone, string email, Guid idEndereco, Endereco endereco, List<Agendamento> agendamentos, Usuario usuario)
        {
            this.IdMedico = idMedico;
            this.Nome = nome;
            this.Cpf = cpf;
            this.Rg = rg;
            this.Crm = crm;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Telefone = telefone;
            this.Email = email;
            this.IdEndereco = idEndereco;
            this.Endereco = endereco;
            this.Agendamentos = agendamentos;
            this.Usuario = usuario;
        }
    }
}
