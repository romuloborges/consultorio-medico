using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Domain.Entity
{
    public class Consulta
    {
        public Guid IdConsulta { get; set; }
        public DateTime DataHoraTerminoConsulta { get; set; }
        public string Observacoes { get; set; }
        public Guid IdAgendamento { get; set; }
        public Agendamento Agendamento { get; set; }

        public Consulta()
        {

        }

        public Consulta(Guid idConsulta, DateTime dataHoraTerminoConsulta, string observacoes, Guid idAgendamento, Agendamento agendamento)
        {
            this.IdConsulta = idConsulta;
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
            this.Observacoes = observacoes;
            this.IdAgendamento = idAgendamento;
            this.Agendamento = agendamento;
        }
    }
}
