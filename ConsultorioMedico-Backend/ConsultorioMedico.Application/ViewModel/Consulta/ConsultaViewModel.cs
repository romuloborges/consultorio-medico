using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class ConsultaViewModel
    {
        public string IdConsulta { get; set; }
        public DateTime DataHoraTerminoConsulta { get; set; }
        public string ReceitaMedica { get; set; }
        public DateTime DuracaoConsulta { get; set; }

        public ConsultaViewModel()
        {

        }

        public ConsultaViewModel(string idConsulta, DateTime dataHoraTerminoConsulta, string receitaMedica, DateTime duracaoConsulta)
        {
            this.IdConsulta = idConsulta;
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
            this.ReceitaMedica = receitaMedica;
            this.DuracaoConsulta = duracaoConsulta;
        }
    }
}
