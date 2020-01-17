using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IConsultaRepository
    {
        Task<bool> CadastrarConsulta(Consulta consulta);
        Task<bool> AtualizarConsulta(Consulta consulta);
        Task<IEnumerable<Consulta>> ObterTodasConsultas();
        Task<IEnumerable<Consulta>> BuscarConsultaPorData(DateTime dataConsulta);
        Task<IEnumerable<Consulta>> ObterConsultasCompletasComFiltro(DateTime dataHoraTerminoConsulta, DateTime dataHoraAgendamento, Guid idPaciente);
        Task<Consulta> BuscarConsultaPorIdAgendamento(Guid idAgendamento);
        Task<Consulta> BuscarConsultaPorId(Guid idConsulta);
        Task<int> ContaConsultasPorPaciente(Guid idPaciente);
        Task<bool> DeletarConsultaPorIdAgendamento(Guid idAgendamento);
        Task<bool> DeletarConsulta(Consulta consulta);
    }
}
