using ConsultorioMedico.Application.ViewModel;
<<<<<<< HEAD
using ConsultorioMedico.Application.ViewModel.Usuario;
=======
>>>>>>> develop
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IUsuarioService
    {
        UsuarioLogadoViewModel ValidarUsuario(UsuarioViewModel usuarioViewModel);
<<<<<<< HEAD
        IEnumerable<UsuarioListarViewModel> ObterTodosUsuariosAtivos();
        Mensagem DeletarUsuario(string id);
=======
>>>>>>> develop
    }
}
