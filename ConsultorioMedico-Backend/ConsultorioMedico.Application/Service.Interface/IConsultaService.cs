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
        string AtualizarConsulta(ConsultaCadastrarViewModel consultaComIdAgendamentoViewModel);
        string DeletarConsulta(ConsultaViewModel consultaComIdAgendamentoViewModel);
        IEnumerable<ConsultaListarViewModel> ObterTodasConsultasCompletas();
    }
}
