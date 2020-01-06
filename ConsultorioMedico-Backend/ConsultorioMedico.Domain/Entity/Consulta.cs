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
        public DateTime DuracaoConsulta { get; set; }
        public Guid IdAgendamento { get; set; }
        public Agendamento Agendamento { get; set; }

        public Consulta()
        {

        }

        public Consulta(Guid idConsulta, DateTime dataHoraTerminoConsulta, string receitaMedica, DateTime duracaoConsulta)
        {
            this.IdConsulta = idConsulta;
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
            this.ReceitaMedica = receitaMedica;
            this.DuracaoConsulta = duracaoConsulta;
        }

        public Consulta(Guid idConsulta, DateTime dataHoraTerminoConsulta, string receitaMedica, DateTime duracaoConsulta, Guid idAgendamento)
        {
            this.IdConsulta = idConsulta;
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
            this.ReceitaMedica = receitaMedica;
            this.DuracaoConsulta = duracaoConsulta;
            this.IdAgendamento = idAgendamento;
        }

        public Consulta(Guid idConsulta, DateTime dataHoraTerminoConsulta, string receitaMedica, DateTime duracaoConsulta, Agendamento agendamento)
        {
            this.IdConsulta = idConsulta;
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
            this.ReceitaMedica = receitaMedica;
            this.DuracaoConsulta = duracaoConsulta;
            this.Agendamento = agendamento;
        }

        public Consulta(Guid idConsulta, DateTime dataHoraTerminoConsulta, string receitaMedica, DateTime duracaoConsulta, Guid idAgendamento, Agendamento agendamento)
        {
            this.IdConsulta = idConsulta;
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
            this.ReceitaMedica = receitaMedica;
            this.DuracaoConsulta = duracaoConsulta;
            this.IdAgendamento = idAgendamento;
            this.Agendamento = agendamento;
        }
    }
}
