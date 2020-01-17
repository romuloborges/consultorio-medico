using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Usuario;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IUsuarioService
    {
        Task<UsuarioLogadoViewModel> ValidarUsuario(string email, string senha);
        Task<IEnumerable<UsuarioListarViewModel>> ObterTodosUsuarios();
        Task<Mensagem> DeletarUsuario(string id);
    }
}
