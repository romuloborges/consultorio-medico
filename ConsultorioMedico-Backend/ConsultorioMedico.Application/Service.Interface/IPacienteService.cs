using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Paciente;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IPacienteService
    {
        Mensagem CadastrarPaciente(PacienteCadastrarViewModel pacienteCadastrarViewModel);
        PacienteAgendarConsultaViewModel ObterPacienteConsulta(string id);
        IEnumerable<PacienteMatSelect> ObterTodosPacientesParaMatSelect();
    }
}
