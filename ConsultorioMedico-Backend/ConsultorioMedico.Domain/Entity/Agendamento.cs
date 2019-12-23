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
        public Guid IdMedico { get; set; }
        public Medico Medico { get; set; }
        public Guid IdPaciente { get; set; }
        public Paciente Paciente { get; set; }
        public Consulta? Consulta { get; set; }

        public Agendamento()
        {

        }

        public Agendamento(Guid idAgendamento, DateTime dataHoraAgendamento, DateTime dataHoraRegistro, Guid idMedico, Medico medico, Guid idPaciente, Paciente paciente, Consulta? consulta)
        {
            this.IdAgendamento = idAgendamento;
            this.DataHoraAgendamento = dataHoraAgendamento;
            this.DataHoraRegistro = dataHoraRegistro;
            this.IdMedico = idMedico;
            this.Medico = medico;
            this.IdPaciente = idPaciente;
            this.Paciente = paciente;
            this.Consulta = consulta;
        }
    }
}
