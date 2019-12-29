using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel.Consulta
{
    public class ConsultaAtendenteViewModel
    {
        public string IdConsulta { get; set; }
        public DateTime DataHoraTerminoConsulta { get; set; }

        public ConsultaAtendenteViewModel()
        {

        }

        public ConsultaAtendenteViewModel(string idConsulta, DateTime dataHoraTerminoConsulta)
        {
            this.IdConsulta = idConsulta;
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
        }
    }
}
