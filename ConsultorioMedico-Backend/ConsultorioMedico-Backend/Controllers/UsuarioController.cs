using ConsultorioMedico.Application;
using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Usuario;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsultorioMedico_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController
    {
        private IUsuarioService usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        // Método para ser revisado
        [Route("validar")]
        [HttpGet]
        public async Task<UsuarioLogadoViewModel> ValidarUsuario([FromQuery] string email, [FromQuery] string senha)
        {
            return await this.usuarioService.ValidarUsuario(email, senha);
        }

        [Route("obterTodosUsuariosAtivos")]
        [HttpGet]
        public async Task<IEnumerable<UsuarioListarViewModel>> ObterTodosUsuarios()
        {
            return await this.usuarioService.ObterTodosUsuarios();
        }

        [Route("deletarUsuario")]
        [HttpDelete]
        public async Task<Mensagem> DeletarUsuario([FromQuery] string id)
        {
            return await this.usuarioService.DeletarUsuario(id);
        }
    }
}
    