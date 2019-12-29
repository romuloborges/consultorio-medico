using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class AgendamentoViewModel
    {
        public DateTime DataHoraAgendamento { get; set; }
        public DateTime DataHoraRegistro { get; set; }
        public string Observacoes { get; set; }
        public string IdMedico { get; set; }
        public string IdPaciente { get; set; }

        public AgendamentoViewModel()
        {

        }

        public AgendamentoViewModel(DateTime dataHoraAgendamento, DateTime dataHoraRegistro, string observacoes, string idMedico, string idPaciente)
        {
            this.DataHoraAgendamento = dataHoraAgendamento;
            this.DataHoraRegistro = dataHoraRegistro;
            this.Observacoes = observacoes;
            this.IdMedico = idMedico;
            this.IdPaciente = idPaciente;
        }
    }
}
