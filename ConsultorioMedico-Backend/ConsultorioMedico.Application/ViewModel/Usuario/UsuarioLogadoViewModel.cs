using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class UsuarioLogadoViewModel
    {
        public string email { get; set; }
        public string nome { get; set; }
        public string tipo { get; set; }

        public UsuarioLogadoViewModel()
        {

        }
        public UsuarioLogadoViewModel(string email, string nome, string tipo)
        {
            this.email = email;
            this.nome = nome;
            this.tipo = tipo;
        }
    }
}
