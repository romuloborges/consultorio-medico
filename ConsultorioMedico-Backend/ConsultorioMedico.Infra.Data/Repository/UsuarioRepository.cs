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
            this.context.Update<Usuario>(usuario);

            return (this.context.SaveChanges() > 0);
        }

        public bool CadastrarUsuario(Usuario usuario)
        {
            this.context.Add<Usuario>(usuario);

            return (this.context.SaveChanges() > 0);
        }

        public bool DeletarUsuario(Usuario usuario)
        {
            this.context.Remove<Usuario>(usuario);

            return (this.context.SaveChanges() > 0);
        }
        public IEnumerable<Usuario> ObterTodosUsuariosAtivos()
        {
            var lista = this.context.Usuario.Include(usuario => usuario.Medico).Include(usuario => usuario.Atendente).Where(usuario => usuario.Ativado).ToList();

            return lista;
        }

        public Usuario ObterUsuarioPorId(Guid id)
        {
            var usuario = this.context.Usuario.AsNoTracking().Include(usuario => usuario.Medico).Include(usuario => usuario.Atendente).FirstOrDefault(usuario => usuario.IdUsuario == id);

            return usuario;
        }

        public Usuario VerificarExistenciaUsuario(string email, string senha)
        {
            Usuario u = this.context.Usuario.Include(usuario => usuario.Medico).Include(usuario => usuario.Atendente).FirstOrDefault(usuario => usuario.Email.Equals(email) && usuario.Senha.Equals(senha));

            return u;
        }
    }
}
