﻿using ConsultorioMedico.Application.ViewModel.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel.Medico
{
    public class MedicoCadastroViewModel
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Crm { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public UsuarioCadastroViewModel Usuario { get; set; }

        public MedicoCadastroViewModel()
        {

        }

        public MedicoCadastroViewModel(string nome, string cpf, string rg, string crm, DateTime dataNascimento, string sexo, string telefone, string email, EnderecoViewModel endereco, UsuarioCadastroViewModel usuario)
        {
            this.Nome = nome;
            this.Cpf = cpf;
            this.Rg = rg;
            this.Crm = crm;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Telefone = telefone;
            this.Email = email;
            this.Endereco = endereco;
            this.Usuario = usuario;
        }
    }
}
