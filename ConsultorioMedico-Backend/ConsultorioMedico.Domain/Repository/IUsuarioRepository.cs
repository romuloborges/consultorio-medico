using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IUsuarioRepository
    {
        Task<bool> CadastrarUsuario(Usuario usuario);
        Task<bool> AtualizarUsuario(Usuario usuario);
        Task<Usuario> ObterUsuarioPorId(Guid id);
        Task<Usuario> ObterUsuarioPorEmail(string email);
        Task<Usuario> VerificarExistenciaUsuario(string email, string senha);
        Task<IEnumerable<Usuario>> ObterTodosUsuarios();
        Task<bool> DeletarUsuario(Usuario usuario);
    }
}
