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
<<<<<<< HEAD
        public string ReceitaMedica { get; set; }
        public AgendamentoParaListagemDeConsultaViewModel agendamentoParaListagemDeConsultaViewModel { get; set; }

        public ConsultaListarViewModel()
        {

        }

        public ConsultaListarViewModel(string idConsulta, DateTime dataHoraTerminoConsulta, string receitaMedica, AgendamentoParaListagemDeConsultaViewModel agendamentoParaListagemDeConsultaViewModel)
        {
            this.IdConsulta = idConsulta;
            this.DataHoraTerminoConsulta = dataHoraTerminoConsulta;
            this.ReceitaMedica = receitaMedica;
            this.agendamentoParaListagemDeConsultaViewModel = agendamentoParaListagemDeConsultaViewModel;
        }
=======
        public string? ReceitaMedica { get; set; }
        public AgendamentoParaListagemDeConsultaViewModel MyProperty { get; set; }
>>>>>>> develop
    }
}
