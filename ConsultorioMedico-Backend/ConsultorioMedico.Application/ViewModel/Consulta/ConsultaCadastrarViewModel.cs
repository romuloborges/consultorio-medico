using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class ConsultaCadastrarViewModel
    {
        public DateTime DataHoraTerminoConsulta { get; set; }
        public string ReceitaMedica { get; set; }
        public string IdAgendamento { get; set; }

        public ConsultaCadastrarViewModel()
        {

        }

        public ConsultaCadastrarViewModel(DateTime dataHoraTerminoConsulta, string receitaMedica, string idAgendamento)
        {
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
            this.ReceitaMedica = receitaMedica;
            this.IdAgendamento = idAgendamento;
        }
    }
}
