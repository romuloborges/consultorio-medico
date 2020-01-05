using ConsultorioMedico.Application;
using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Usuario;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public UsuarioLogadoViewModel ValidarUsuario([FromQuery] string email, [FromQuery] string senha)
        {
            return this.usuarioService.ValidarUsuario(email, senha);
        }

        [Route("obterTodosUsuariosAtivos")]
        [HttpGet]
        public IEnumerable<UsuarioListarViewModel> ObterTodosUsuarios()
        {
            return this.usuarioService.ObterTodosUsuarios();
        }

        [Route("deletarUsuario")]
        [HttpDelete]
        public Mensagem DeletarUsuario([FromQuery] string id)
        {
            return this.usuarioService.DeletarUsuario(id);
        }
    }
}
    