using ConsultorioMedico.Application;
using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Usuario;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [HttpPost]
        public UsuarioLogadoViewModel ValidarUsuario([FromBody] UsuarioViewModel usuarioViewModel)
        {
            return this.usuarioService.ValidarUsuario(usuarioViewModel);
        }

        [Route("obterTodosUsuariosAtivos")]
        [HttpGet]
        public IEnumerable<UsuarioListarViewModel> ObterTodosUsuarios()
        {
            return this.usuarioService.ObterTodosUsuariosAtivos();
        }

        [Route("deletarUsuario")]
        [HttpDelete]
        public Mensagem DeletarUsuario([FromQuery] string id)
        {
            return this.usuarioService.DeletarUsuario(id);
        }
    }
}
    