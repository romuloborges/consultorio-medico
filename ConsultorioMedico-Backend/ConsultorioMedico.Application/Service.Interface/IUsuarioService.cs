using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IUsuarioService
    {
        UsuarioLogadoViewModel ValidarUsuario(string email, string senha);
        IEnumerable<UsuarioListarViewModel> ObterTodosUsuarios();
        Mensagem DeletarUsuario(string id);
    }
}
