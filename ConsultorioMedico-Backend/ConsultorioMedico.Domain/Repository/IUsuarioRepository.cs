using System;
using System.Collections.Generic;
using System.Text;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IUsuarioRepository
    {
        bool CadastrarUsuario(Usuario usuario);
        bool AtualizarUsuario(Usuario usuario);
        Usuario ObterUsuarioPorId(Guid id);
        Usuario ObterUsuarioPorEmail(string email);
        Usuario VerificarExistenciaUsuario(string email, string senha);
        IEnumerable<Usuario> ObterTodosUsuarios();
        bool DeletarUsuario(Usuario usuario);
    }
}
