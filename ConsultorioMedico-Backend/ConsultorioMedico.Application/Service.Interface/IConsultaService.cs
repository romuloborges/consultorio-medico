using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Consulta;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IConsultaService
    {
        Task<Mensagem> CadastrarConsulta(ConsultaCadastrarViewModel consultaComIdAgendamentoViewModel);
        Task<Mensagem> AtualizarConsulta(ConsultaComIdAgendamentoViewModel consultaViewModel);
        Task<Mensagem> DeletarConsulta(string id);
        Task<IEnumerable<ConsultaListarViewModel>> ObterConsultasCompletasComFiltro(DateTime dataHoraTerminoConsulta, DateTime dataHoraAgendamento, string idPaciente);
    }
}
