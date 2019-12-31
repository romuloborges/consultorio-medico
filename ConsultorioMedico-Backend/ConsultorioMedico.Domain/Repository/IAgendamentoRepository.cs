using System;
using System.Collections.Generic;
using System.Text;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IAgendamentoRepository
    {
        bool CadastrarAgendamento(Agendamento agendamento);
        bool AtualizarAgendamento(Agendamento agendamento);
        Agendamento BuscarAgendamentoPorId(Guid idAgendamento);
        IEnumerable<Agendamento> BuscarAgendamentoPorDataAgendada(DateTime dataAgendada);
        IEnumerable<Agendamento> BuscarAgendamentoPorDataRegistro(DateTime dataRegistro);
        IEnumerable<Agendamento> BuscarAgendamentoPorMedico(Guid medico);
        IEnumerable<Agendamento> BuscarAgendamentoComFiltro(DateTime? dataHoraInicio, DateTime? dataHorafim, Guid? idPaciente, Guid? idMedico);
        // Se já foi consultado, mostra do lado
        IEnumerable<Agendamento> BuscarAgendamentoPorPaciente(Guid paciente);
        int ContarAgendamentosPaciente(Guid paciente);
        // Este médico já possui um compromisso nessa data e hora
        bool VerificaExistenciaAgendamentoMedico(Guid idMedico, DateTime dataAgendada);
        // Este paciente já possui um compromisso nessa data e hora
        bool VerificaExistenciaAgendamentoPaciente(Guid idPaciente, DateTime dataAgendada);
        
        bool DeletarAgendamento(Agendamento agendamento);
    }
}
