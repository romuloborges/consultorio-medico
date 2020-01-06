using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Domain.Entity
{
    public class Agendamento
    {
        public Guid IdAgendamento { get; set; }
        public DateTime DataHoraAgendamento { get; set; }
        public DateTime DataHoraRegistro { get; set; }
        public string Observacoes { get; set; }
        public Guid IdMedico { get; set; }
        public Medico Medico { get; set; }
        public Guid IdPaciente { get; set; }
        public Paciente Paciente { get; set; }
        public Consulta? Consulta { get; set; }

        public Agendamento()
        {

        }

        public Agendamento(DateTime dataHoraAgendamento, DateTime dataHoraRegistro, string observacoes, Guid idMedico, Guid idPaciente)
        {
            this.DataHoraAgendamento = dataHoraAgendamento;
            this.DataHoraRegistro = dataHoraRegistro;
            this.Observacoes = observacoes;
            this.IdMedico = idMedico;
            this.IdPaciente = idPaciente;
        }

        public Agendamento(Guid idAgendamento, DateTime dataHoraAgendamento, DateTime dataHoraRegistro, Guid idMedico, Guid idPaciente)
        {
            this.IdAgendamento = idAgendamento;
            this.DataHoraAgendamento = dataHoraAgendamento;
            this.DataHoraRegistro = dataHoraRegistro;
            this.IdMedico = idMedico;
            this.IdPaciente = idPaciente;
        }
        public Agendamento(Guid idAgendamento, DateTime dataHoraAgendamento, DateTime dataHoraRegistro, string observacoes, Guid idMedico, Guid idPaciente)
        {
            this.IdAgendamento = idAgendamento;
            this.DataHoraAgendamento = dataHoraAgendamento;
            this.DataHoraRegistro = dataHoraRegistro;
            this.Observacoes = observacoes;
            this.IdMedico = idMedico;
            this.IdPaciente = idPaciente;
        }

        public Agendamento(Guid idAgendamento, DateTime dataHoraAgendamento, DateTime dataHoraRegistro, string observacoes, Medico medico, Paciente paciente, Consulta consulta)
        {
            this.IdAgendamento = idAgendamento;
            this.DataHoraAgendamento = dataHoraAgendamento;
            this.DataHoraRegistro = dataHoraRegistro;
            this.Observacoes = observacoes;
            this.Medico = medico;
            this.Paciente = paciente;
            this.Consulta = consulta;
        }

        public Agendamento(Guid idAgendamento, DateTime dataHoraAgendamento, DateTime dataHoraRegistro, string observacoes, Guid idMedico, Medico medico, Guid idPaciente, Paciente paciente, Consulta consulta)
        {
            this.IdAgendamento = idAgendamento;
            this.DataHoraAgendamento = dataHoraAgendamento;
            this.DataHoraRegistro = dataHoraRegistro;
            this.Observacoes = observacoes;
            this.IdMedico = idMedico;
            this.Medico = medico;
            this.IdPaciente = idPaciente;
            this.Paciente = paciente;
            this.Consulta = consulta;
        }
    }
}
