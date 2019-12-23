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
        IEnumerable<Agendamento> BuscarAgendamentoPorDataAgendada(DateTime dataAgendada);
        IEnumerable<Agendamento> BuscarAgendamentoPorDataRegistro(DateTime dataRegistro);
        IEnumerable<Agendamento> BuscarAgendamentoPorMedico(Medico medico);
        // Se já foi consultado, mostra do lado
        IEnumerable<Agendamento> BuscarAgendamentoPorPaciente(Paciente paciente);
        // Este médico já possui um compromisso nessa data e hora
        bool VerificaExistenciaAgendamento(Medico medico, DateTime dataAgendada);
        // Este paciente já possui um compromisso nessa data e hora
        bool VerificaExistenciaAgendamento(Paciente paciente, DateTime dataAgendada);
        
        bool DeletarAgendamento(Agendamento agendamento);
    }
}
