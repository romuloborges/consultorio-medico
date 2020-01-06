using System;
using System.Collections.Generic;
using System.Text;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IConsultaRepository
    {
        bool CadastrarConsulta(Consulta consulta);
        bool AtualizarConsulta(Consulta consulta);
        IEnumerable<Consulta> ObterTodasConsultas();
        IEnumerable<Consulta> BuscarConsultaPorData(DateTime dataConsulta);
        IEnumerable<Consulta> ObterConsultasCompletasComFiltro(DateTime dataHoraTerminoConsulta, DateTime dataHoraAgendamento, Guid idPaciente);
        Consulta BuscarConsultaPorIdAgendamento(Guid idAgendamento);
        Consulta BuscarConsultaPorId(Guid idConsulta);
        int ContaConsultasPorPaciente(Guid idPaciente);
        bool DeletarConsultaPorIdAgendamento(Guid idAgendamento);
        bool DeletarConsulta(Consulta consulta);
    }
}
