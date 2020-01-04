using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Usuario;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IUsuarioService
    {
        UsuarioLogadoViewModel ValidarUsuario(UsuarioViewModel usuarioViewModel);
        IEnumerable<UsuarioListarViewModel> ObterTodosUsuariosAtivos();
        Mensagem DeletarUsuario(string id);
    }
}
