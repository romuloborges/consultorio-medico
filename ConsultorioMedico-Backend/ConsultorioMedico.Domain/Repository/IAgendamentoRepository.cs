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

        // Quando o id passado corresponde ao Id de um médico, os agendamentos feitos para este médico na data passada são selecionados.
        // Quando o id passado não corresponde ao Id de um médico, todos os agendamentos para a data atual são selecionados.
        IEnumerable<Agendamento> BuscarAgendamentoPorDataAgendadaComIdMedico(DateTime dataAgendada, Guid id);
        IEnumerable<Agendamento> BuscarAgendamentoPorDataRegistro(DateTime dataRegistro);
        IEnumerable<Agendamento> BuscarAgendamentoPorMedico(Guid medico);
        IEnumerable<Agendamento> BuscarAgendamentoSemConsultaComFiltro(DateTime dataInicio, DateTime datafim, Guid idPaciente, Guid idMedico);
        IEnumerable<Agendamento> BuscarAgendamentoEntreDataEHora(DateTime dataHoraInicio, DateTime dataHoraFim, Guid idPaciente, Guid idMedico);
        // Se já foi consultado, mostra do lado
        IEnumerable<Agendamento> BuscarAgendamentoPorPaciente(Guid paciente);
        int ContarAgendamentosPaciente(Guid paciente);
        // Este médico já possui um compromisso nessa data e hora
        int QuantidadeAgendamentosMedico(Guid idMedico);
        bool VerificaExistenciaAgendamentoMedico(Guid idMedico, DateTime dataAgendada);
        // Este paciente já possui um compromisso nessa data e hora
        bool VerificaExistenciaAgendamentoPaciente(Guid idPaciente, DateTime dataAgendada);
        
        bool DeletarAgendamento(Agendamento agendamento);
    }
}
