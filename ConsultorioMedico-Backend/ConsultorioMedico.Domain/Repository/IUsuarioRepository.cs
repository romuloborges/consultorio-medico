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
        Usuario VerificarExistenciaUsuario(string email, string senha);
        bool DeletarUsuario(Usuario usuario);
    }
}
