using System;
using System.Collections.Generic;
using System.Text;
using ConsultorioMedico.Application.ViewModel;
<<<<<<< HEAD
using ConsultorioMedico.Application.ViewModel.Consulta;
=======
>>>>>>> develop

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IConsultaService
    {
        Mensagem CadastrarConsulta(ConsultaCadastrarViewModel consultaComIdAgendamentoViewModel);
<<<<<<< HEAD
        Mensagem AtualizarConsulta(ConsultaComIdAgendamentoViewModel consultaViewModel);
        string DeletarConsulta(ConsultaViewModel consultaComIdAgendamentoViewModel);
        Mensagem DeletarConsulta(string id);
        IEnumerable<ConsultaListarViewModel> ObterConsultasCompletasComFiltro(DateTime dataHoraTerminoConsulta, DateTime dataHoraAgendamento, string idPaciente);
=======
        string AtualizarConsulta(ConsultaCadastrarViewModel consultaComIdAgendamentoViewModel);
        string DeletarConsulta(ConsultaViewModel consultaComIdAgendamentoViewModel);
>>>>>>> develop
    }
}
