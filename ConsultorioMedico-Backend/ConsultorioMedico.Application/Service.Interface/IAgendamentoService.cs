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
        IEnumerable<AgendamentoListarViewModel> BuscarAgendamentoComFiltro(DateTime dataHoraInicio, DateTime dataHoraFim, string idPaciente, string idMedico, bool aindaNaoConsultados);
        Mensagem AtualizarAgendamento(AgendamentoComIdViewModel agendamentoComIdViewModel);
        //string DeletarAgendamento(AgendamentoComIdViewModel agendamentoComIdViewModel);
        Mensagem DeletarAgendamento(string id);
    }
}
