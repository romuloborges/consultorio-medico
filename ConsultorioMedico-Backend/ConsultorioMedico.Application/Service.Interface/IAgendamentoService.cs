using ConsultorioMedico.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IAgendamentoService
    {
        Mensagem CadastrarAgendamento(AgendamentoViewModel agendamentoViewModel);
        IEnumerable<AgendamentoListarViewModel> BuscarAgendamentoPorDataAgendada(DateTime dataAgendada);
        string AtualizarAgendamento(AgendamentoComIdViewModel agendamentoComIdViewModel);
        //string DeletarAgendamento(AgendamentoComIdViewModel agendamentoComIdViewModel);
        string DeletarAgendamento(string id);
    }
}
