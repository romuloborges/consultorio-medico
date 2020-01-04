using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Domain.Entity
{
    public class Atendente
    {
        public Guid IdAtendente { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public Guid IdEndereco { get; set; }
        public Endereco Endereco { get; set; }
        public Usuario Usuario { get; set; }

        public Atendente()
        {

        }

<<<<<<< HEAD
        public Atendente(string nome, DateTime dataNascimento, string sexo, string cpf, string rg, string email, string telefone, Guid idEndereco)
        {
            this.Nome = nome;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Cpf = cpf;
            this.Rg = rg;
            this.Email = email;
            this.Telefone = telefone;
            this.IdEndereco = idEndereco;
        }

=======
>>>>>>> develop
        public Atendente(Guid idAtendente, string nome, DateTime dataNascimento, string sexo, string cpf, string rg, string email, string telefone, Guid idEndereco, Endereco endereco, Usuario usuario)
        {
            this.IdAtendente = idAtendente;
            this.Nome = nome;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Cpf = cpf;
            this.Rg = rg;
            this.Email = email;
            this.Telefone = telefone;
            this.IdEndereco = idEndereco;
            this.Endereco = endereco;
            this.Usuario = usuario;
        }
    }
}
