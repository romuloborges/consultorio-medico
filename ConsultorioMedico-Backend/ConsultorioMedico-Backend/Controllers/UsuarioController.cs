using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
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

        [Route("validar")]
        [HttpPost]
        public UsuarioLogadoViewModel ValidarUsuario(UsuarioViewModel usuarioViewModel)
        {
            return this.usuarioService.ValidarUsuario(usuarioViewModel);
        }
    }
}
    