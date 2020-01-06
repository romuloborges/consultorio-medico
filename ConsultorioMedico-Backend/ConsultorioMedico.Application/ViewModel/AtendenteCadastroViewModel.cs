using ConsultorioMedico.Application.ViewModel.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class AtendenteCadastroViewModel
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public UsuarioCadastroViewModel Usuario { get; set; }

        public AtendenteCadastroViewModel()
        {

        }

        public AtendenteCadastroViewModel(string nome, DateTime dataNascimento, string sexo, string cpf, string rg, string email, string telefone, EnderecoViewModel endereco, UsuarioCadastroViewModel usuario)
        {
            this.Nome = nome;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Cpf = cpf;
            this.Rg = rg;
            this.Email = email;
            this.Telefone = telefone;
            this.Endereco = endereco;
            this.Usuario = usuario;
        }
    }
}
