using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Medico;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IMedicoService
    {
        Task<IEnumerable<MedicoMatSelectViewModel>> ObterTodosMedicosParaMatSelect();
        Task<Mensagem> CadastrarMedico(MedicoCadastroViewModel medicoCadastroViewModel);
    }
}
