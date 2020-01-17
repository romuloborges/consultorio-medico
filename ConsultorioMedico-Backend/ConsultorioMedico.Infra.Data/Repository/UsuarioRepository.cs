using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ConsultorioMedico.Infra.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private ConsultorioMedicoContext context;
        public UsuarioRepository(ConsultorioMedicoContext context)
        {
            this.context = context;
        }

        public async Task<bool> CadastrarUsuario(Usuario usuario)
        {
            await this.context.AddAsync<Usuario>(usuario);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<bool> AtualizarUsuario(Usuario usuario)
        {
            this.context.Update<Usuario>(usuario);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<IEnumerable<Usuario>> ObterTodosUsuarios()
        {
            var lista = await this.context.Usuario.Include(usuario => usuario.Medico).Include(usuario => usuario.Atendente).ToListAsync();

            return lista;
        }

        public async Task<Usuario> ObterUsuarioPorId(Guid id)
        {
            var usuario = await this.context.Usuario.AsNoTracking().Include(usuario => usuario.Medico).Include(usuario => usuario.Atendente).FirstOrDefaultAsync(usuario => usuario.IdUsuario == id);

            return usuario;
        }

        public async Task<Usuario> VerificarExistenciaUsuario(string email, string senha)
        {
            Usuario u = await this.context.Usuario.Include(usuario => usuario.Medico).Include(usuario => usuario.Atendente).FirstOrDefaultAsync(usuario => usuario.Email.Equals(email) && usuario.Senha.Equals(senha));

            return u;
        }

        public async Task<bool> DeletarUsuario(Usuario usuario)
        {
            this.context.Remove<Usuario>(usuario);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<Usuario> ObterUsuarioPorEmail(string email)
        {
            var usuario = await this.context.Set<Usuario>().FirstOrDefaultAsync(usuario => usuario.Email.Equals(email));

            return usuario;
        }
    }
}
