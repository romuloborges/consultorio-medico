using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class AgendamentoViewModel
    {
        public DateTime DataHoraAgendamento { get; set; }
        public DateTime DataHoraRegistro { get; set; }
        public Guid IdMedico { get; set; }
        public Guid IdPaciente { get; set; }

        public AgendamentoViewModel()
        {

        }

        public AgendamentoViewModel(DateTime dataHoraAgendamento, DateTime dataHoraRegistro, Guid idMedico, Guid idPaciente)
        {
            this.DataHoraAgendamento = dataHoraAgendamento;
            this.DataHoraRegistro = dataHoraRegistro;
            this.IdMedico = idMedico;
            this.IdPaciente = idPaciente;
        }
    }
}
