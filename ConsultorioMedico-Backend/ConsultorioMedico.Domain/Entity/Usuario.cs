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
<<<<<<< HEAD
        public bool Ativado { get; set; }
=======
>>>>>>> develop
        public Guid? IdMedico { get; set; }
        public Medico? Medico { get; set; }
        public Guid? IdAtendente { get; set; }
        public Atendente? Atendente { get; set; }

        public Usuario()
        {

        }

<<<<<<< HEAD
        public Usuario(string email, string senha, string tipo, bool ativado, Guid? idMedico, Guid? idAtendente)
        {
            this.Email = email;
            this.Senha = senha;
            this.Tipo = tipo;
            this.Ativado = ativado;
            this.IdMedico = idMedico;
            this.IdAtendente = idAtendente;
        }

        public Usuario(Guid idUsuario, string email, string senha, string tipo, bool ativado, Medico? medico, Atendente? atendente)
=======
        public Usuario(Guid idUsuario, string email, string senha, string tipo, Medico? medico, Atendente? atendente)
>>>>>>> develop
        {
            this.IdUsuario = idUsuario;
            this.Email = email;
            this.Senha = senha;
            this.Tipo = tipo;
<<<<<<< HEAD
            this.Ativado = ativado;
=======
>>>>>>> develop
            this.Medico = medico;
            this.Atendente = atendente;
        }
    }
}
