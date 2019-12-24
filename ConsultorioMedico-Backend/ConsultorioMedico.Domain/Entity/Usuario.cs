using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Domain.Entity
{
    public class Usuario
    {
        public Guid IdUsuario { get; set; }
        public string Email { get; set; }
        public string  Senha { get; set; }
        public string Tipo { get; set; }
        public Guid? IdMedico { get; set; }
        public Medico? Medico { get; set; }
        public Guid? IdAtendente { get; set; }
        public Atendente? Atendente { get; set; }

        public Usuario()
        {

        }

        public Usuario(Guid idUsuario, string email, string senha, string tipo, Medico? medico, Atendente? atendente)
        {
            this.IdUsuario = idUsuario;
            this.Email = email;
            this.Senha = senha;
            this.Tipo = tipo;
            this.Medico = medico;
            this.Atendente = atendente;
        }
    }
}
