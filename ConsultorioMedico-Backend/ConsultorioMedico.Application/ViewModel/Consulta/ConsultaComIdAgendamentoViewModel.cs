using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class ConsultaComIdAgendamentoViewModel
    {
        public string IdConsulta { get; set; }
        public DateTime DataHoraTerminoConsulta { get; set; }
        public string Observacoes { get; set; }
        public string IdAgendamento { get; set; }

        public ConsultaComIdAgendamentoViewModel()
        {

        }

        public ConsultaComIdAgendamentoViewModel(string idConsulta, DateTime dataHoraTerminoConsulta, string observacoes, string idAgendamento)
        {
            this.IdConsulta = idConsulta;
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
            this.Observacoes = observacoes;
            this.IdAgendamento = idAgendamento;
        }
    }
}
