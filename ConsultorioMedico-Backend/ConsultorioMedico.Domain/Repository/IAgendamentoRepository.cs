using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IAgendamentoRepository
    {
        Task<bool> CadastrarAgendamento(Agendamento agendamento);
        Task<bool> AtualizarAgendamento(Agendamento agendamento);
        Task<Agendamento> BuscarAgendamentoPorId(Guid idAgendamento);

        // Quando o id passado corresponde ao Id de um médico, os agendamentos feitos para este médico na data passada são selecionados.
        // Quando o id passado não corresponde ao Id de um médico, todos os agendamentos para a data atual são selecionados.
        Task<IEnumerable<Agendamento>> BuscarAgendamentoPorDataAgendadaComIdMedico(DateTime dataAgendada, Guid id);
        Task<IEnumerable<Agendamento>> BuscarAgendamentoPorDataRegistro(DateTime dataRegistro);
        Task<IEnumerable<Agendamento>> BuscarAgendamentoPorMedico(Guid medico);
        Task<IEnumerable<Agendamento>> BuscarAgendamentoSemConsultaComFiltro(DateTime dataInicio, DateTime datafim, Guid idPaciente, Guid idMedico);
        Task<IEnumerable<Agendamento>> BuscarAgendamentoEntreDataEHora(DateTime dataHoraInicio, DateTime dataHoraFim, Guid idPaciente, Guid idMedico);
        // Se já foi consultado, mostra do lado
        Task<IEnumerable<Agendamento>> BuscarAgendamentoPorPaciente(Guid paciente);
        Task<int> ContarAgendamentosPaciente(Guid paciente);
        // Este médico já possui um compromisso nessa data e hora
        Task<int> QuantidadeAgendamentosMedico(Guid idMedico);
        Task<bool> VerificaExistenciaAgendamentoMedico(Guid idMedico, DateTime dataAgendada);
        // Este paciente já possui um compromisso nessa data e hora
        Task<bool> VerificaExistenciaAgendamentoPaciente(Guid idPaciente, DateTime dataAgendada);
        
        Task<bool> DeletarAgendamento(Agendamento agendamento);
    }
}
