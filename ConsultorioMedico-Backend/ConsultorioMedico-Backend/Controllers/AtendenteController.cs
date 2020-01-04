using ConsultorioMedico.Application;
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
    public class AtendenteController
    {
        private IAtendenteService atendenteService;

        public AtendenteController(IAtendenteService atendenteService)
        {
            this.atendenteService = atendenteService;
        }

        [Route("cadastrar")]
        [HttpPost]
        public Mensagem CadastrarAtendente(AtendenteCadastroViewModel atendenteCadastroViewModel)
        {
            return this.atendenteService.CadastrarAtendente(atendenteCadastroViewModel);
        }
    }
}
