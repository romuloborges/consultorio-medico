using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class ConsultaCadastrarViewModel
    {
        public DateTime DataHoraTerminoConsulta { get; set; }
        public string Observacoes { get; set; }
        public string IdAgendamento { get; set; }

        public ConsultaCadastrarViewModel()
        {

        }

        public ConsultaCadastrarViewModel(DateTime dataHoraTerminoConsulta, string observacoes, string idAgendamento)
        {
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
            this.Observacoes = observacoes;
            this.IdAgendamento = idAgendamento;
        }
    }
}
