using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Medico;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IMedicoService
    {
        IEnumerable<MedicoMatSelectViewModel> ObterTodosMedicosParaMatSelect();
        Mensagem CadastrarMedico(MedicoCadastroViewModel medicoCadastroViewModel);
    }
}
