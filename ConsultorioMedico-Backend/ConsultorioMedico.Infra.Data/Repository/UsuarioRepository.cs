using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace ConsultorioMedico.Infra.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private ConsultorioMedicoContext context;
        public UsuarioRepository(ConsultorioMedicoContext context)
        {
            this.context = context;
        }

        public bool AtualizarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public bool CadastrarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public bool DeletarUsuario(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Usuario VerificarExistenciaUsuario(string email, string senha)
        {
            Usuario u = this.context.Usuario.Include(usuario => usuario.Medico).Include(usuario => usuario.Atendente).FirstOrDefault(usuario => usuario.Email.Equals(email) && usuario.Senha.Equals(senha));

            return u;
        }
    }
}
