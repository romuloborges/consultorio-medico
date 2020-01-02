using ConsultorioMedico.Application.ViewModel.Agendamento;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel.Consulta
{
    public class ConsultaListarViewModel
    {
        public string IdConsulta { get; set; }
        public DateTime DataHoraTerminoConsulta { get; set; }
        public string? ReceitaMedica { get; set; }
        public AgendamentoParaListagemDeConsultaViewModel MyProperty { get; set; }
    }
}
