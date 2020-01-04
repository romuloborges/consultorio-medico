using ConsultorioMedico.Application;
using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Medico;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultorioMedico_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicoController
    {
        private IMedicoService medicoService;
        public MedicoController(IMedicoService medicoService)
        {
            this.medicoService = medicoService;
        }

        [Route("cadastrar")]
        [HttpPost]
        public Mensagem CadastrarMedico(MedicoCadastroViewModel medicoCadastroViewModel)
        {
            return this.medicoService.CadastrarMedico(medicoCadastroViewModel);
        }

        [HttpGet]
        public IEnumerable<MedicoMatSelectViewModel> ObterTodosMedicosListagem()
        {
            return this.medicoService.ObterTodosMedicosParaMatSelect();
        }
    }
}
