using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class ConsultaViewModel
    {
        public string IdConsulta { get; set; }
        public DateTime DataHoraTerminoConsulta { get; set; }
        public string Observacoes { get; set; }

        public ConsultaViewModel()
        {

        }

        public ConsultaViewModel(string idConsulta, DateTime dataHoraTerminoConsulta, string observacoes)
        {
            this.IdConsulta = idConsulta;
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
            this.Observacoes = observacoes;
        }
    }
}
