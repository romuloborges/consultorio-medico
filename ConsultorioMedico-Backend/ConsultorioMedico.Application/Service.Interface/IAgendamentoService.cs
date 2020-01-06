using ConsultorioMedico.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IAgendamentoService
    {
        Mensagem CadastrarAgendamento(AgendamentoCadastrarViewModel agendamentoViewModel);
        IEnumerable<AgendamentoListarViewModel> BuscarAgendamentoPorDataAgendadaComIdMedico(DateTime dataAgendada, string id);
        IEnumerable<AgendamentoListarViewModel> BuscarAgendamentoComFiltro(DateTime dataHoraInicio, DateTime dataHoraFim, string idPaciente, string idMedico);
        Mensagem AtualizarAgendamento(AgendamentoComIdViewModel agendamentoComIdViewModel);
        Mensagem DeletarAgendamento(string id);
    }
}
