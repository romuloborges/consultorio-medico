using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class AgendamentoViewModel
    {
        public DateTime DataHoraAgendamento { get; set; }
        public DateTime DataHoraRegistro { get; set; }
        public string IdMedico { get; set; }
        public string IdPaciente { get; set; }

        public AgendamentoViewModel()
        {

        }

        public AgendamentoViewModel(DateTime dataHoraAgendamento, DateTime dataHoraRegistro, string idMedico, string idPaciente)
        {
            this.DataHoraAgendamento = dataHoraAgendamento;
            this.DataHoraRegistro = dataHoraRegistro;
            this.IdMedico = idMedico;
            this.IdPaciente = idPaciente;
        }
    }
}
