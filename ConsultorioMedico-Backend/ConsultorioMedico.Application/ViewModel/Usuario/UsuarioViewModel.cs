using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class UsuarioViewModel
    {
        public string email { get; set; }
        public string senha { get; set; }

        public UsuarioViewModel()
        {

        }
        public UsuarioViewModel(string email, string senha)
        {
            this.email = email;
            this.senha = senha;
        }
    }
}
