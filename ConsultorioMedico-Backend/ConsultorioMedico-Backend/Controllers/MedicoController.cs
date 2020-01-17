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
        public async Task<Mensagem> CadastrarMedico(MedicoCadastroViewModel medicoCadastroViewModel)
        {
            return await this.medicoService.CadastrarMedico(medicoCadastroViewModel);
        }

        [HttpGet]
        public async Task<IEnumerable<MedicoMatSelectViewModel>> ObterTodosMedicosListagem()
        {
            return await this.medicoService.ObterTodosMedicosParaMatSelect();
        }
    }
}
