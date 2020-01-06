using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class UsuarioLogadoViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }

        public UsuarioLogadoViewModel()
        {

        }
        public UsuarioLogadoViewModel(string id, string email, string nome, string tipo)
        {
            this.Id = id;
            this.Email = email;
            this.Nome = nome;
            this.Tipo = tipo;
        }
    }
}
