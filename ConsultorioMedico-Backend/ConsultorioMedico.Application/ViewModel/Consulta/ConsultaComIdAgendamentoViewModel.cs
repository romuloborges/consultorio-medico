using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class ConsultaComIdAgendamentoViewModel
    {
        public string IdConsulta { get; set; }
        public DateTime DataHoraTerminoConsulta { get; set; }
        public string ReceitaMedica { get; set; }
        public DateTime DuracaoConsulta { get; set; }
        public string IdAgendamento { get; set; }

        public ConsultaComIdAgendamentoViewModel()
        {

        }

        public ConsultaComIdAgendamentoViewModel(string idConsulta, DateTime dataHoraTerminoConsulta, string receitaMedica, DateTime duracaoConsulta, string idAgendamento)
        {
            this.IdConsulta = idConsulta;
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
            this.ReceitaMedica = receitaMedica;
            this.DuracaoConsulta = duracaoConsulta;
            this.IdAgendamento = idAgendamento;
        }
    }
}
