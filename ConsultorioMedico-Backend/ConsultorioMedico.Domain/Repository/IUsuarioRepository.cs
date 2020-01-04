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
<<<<<<< HEAD
        Usuario ObterUsuarioPorId(Guid id);
        Usuario VerificarExistenciaUsuario(string email, string senha);
        IEnumerable<Usuario> ObterTodosUsuariosAtivos();
=======
        Usuario VerificarExistenciaUsuario(string email, string senha);
>>>>>>> develop
        bool DeletarUsuario(Usuario usuario);
    }
}
