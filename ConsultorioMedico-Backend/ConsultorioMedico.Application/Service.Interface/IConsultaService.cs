using System;
using System.Collections.Generic;
using System.Text;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Consulta;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IConsultaService
    {
        Mensagem CadastrarConsulta(ConsultaCadastrarViewModel consultaComIdAgendamentoViewModel);
        Mensagem AtualizarConsulta(ConsultaComIdAgendamentoViewModel consultaViewModel);
        Mensagem DeletarConsulta(string id);
        IEnumerable<ConsultaListarViewModel> ObterConsultasCompletasComFiltro(DateTime dataHoraTerminoConsulta, DateTime dataHoraAgendamento, string idPaciente);
    }
}
