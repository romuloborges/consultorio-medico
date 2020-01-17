using ConsultorioMedico.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IAgendamentoService
    {
        Task<Mensagem> CadastrarAgendamento(AgendamentoCadastrarViewModel agendamentoViewModel);
        Task<IEnumerable<AgendamentoListarViewModel>> BuscarAgendamentoPorDataAgendadaComIdMedico(DateTime dataAgendada, string id);
        Task<IEnumerable<AgendamentoListarViewModel>> BuscarAgendamentoComFiltro(DateTime dataHoraInicio, DateTime dataHoraFim, string idPaciente, string idMedico);
        Task<Mensagem> AtualizarAgendamento(AgendamentoComIdViewModel agendamentoComIdViewModel);
        Task<Mensagem> DeletarAgendamento(string id);
    }
}
