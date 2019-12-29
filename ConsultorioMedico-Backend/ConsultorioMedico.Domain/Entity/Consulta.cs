using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Domain.Entity
{
    public class Consulta
    {
        public Guid IdConsulta { get; set; }
        public DateTime DataHoraTerminoConsulta { get; set; }
        public string ReceitaMedica { get; set; }
        public Guid IdAgendamento { get; set; }
        public Agendamento Agendamento { get; set; }

        public Consulta()
        {

        }

        public Consulta(Guid idConsulta, DateTime dataHoraTerminoConsulta, string receitaMedica)
        {
            this.IdConsulta = idConsulta;
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
            this.ReceitaMedica = receitaMedica;
        }

        public Consulta(Guid idConsulta, DateTime dataHoraTerminoConsulta, string receitaMedica, Guid idAgendamento)
        {
            this.IdConsulta = idConsulta;
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
            this.ReceitaMedica = receitaMedica;
            this.IdAgendamento = idAgendamento;
        }

        public Consulta(Guid idConsulta, DateTime dataHoraTerminoConsulta, string receitaMedica, Guid idAgendamento, Agendamento agendamento)
        {
            this.IdConsulta = idConsulta;
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
            this.ReceitaMedica = receitaMedica;
            this.IdAgendamento = idAgendamento;
            this.Agendamento = agendamento;
        }
    }
}
