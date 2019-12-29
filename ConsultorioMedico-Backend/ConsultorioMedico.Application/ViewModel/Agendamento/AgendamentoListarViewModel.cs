using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class AgendamentoListarViewModel
    {
        public string IdAgendamento { get; set; }
        public DateTime DataHoraAgendamento { get; set; }
        public DateTime DataHoraRegistro { get; set; }
        public string Observacoes { get; set; }
        public MedicoMatSelectViewModel MedicoListarViewModel { get; set; }
        public PacienteListarViewModel PacienteListarViewModel { get; set; }
        public ConsultaViewModel? ConsultaViewModel { get; set; }

        public AgendamentoListarViewModel()
        {

        }

        public AgendamentoListarViewModel(string idAgendamento, DateTime dataHoraAgendamento, DateTime dataHoraRegistro, string observacoes, MedicoMatSelectViewModel medicoListarViewModel, PacienteListarViewModel pacienteListarViewModel, ConsultaViewModel? consultaViewModel)
        {
            this.IdAgendamento = idAgendamento;
            this.DataHoraAgendamento = dataHoraAgendamento;
            this.DataHoraRegistro = dataHoraRegistro;
            this.Observacoes = observacoes;
            this.MedicoListarViewModel = medicoListarViewModel;
            this.PacienteListarViewModel = pacienteListarViewModel;
            this.ConsultaViewModel = consultaViewModel;
        }
    }
}
