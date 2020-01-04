using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel.Usuario
{
    public class UsuarioCadastroViewModel
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Tipo { get; set; }

        public UsuarioCadastroViewModel()
        {

        }

        public UsuarioCadastroViewModel(string email, string senha, string tipo)
        {
            this.Email = email;
            this.Senha = senha;
            this.Tipo = tipo;
        }
    }
}
