using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel.Agendamento
{
    public class AgendamentoParaListagemDeConsultaViewModel
    {
        public string IdAgendamento { get; set; }
        public DateTime DataHoraAgendamento { get; set; }
        public DateTime DataHoraRegistro { get; set; }
        public string Observacoes { get; set; }
        public MedicoMatSelectViewModel Medico { get; set; }
        public PacienteListarViewModel Paciente { get; set; }

        public AgendamentoParaListagemDeConsultaViewModel()
        {

        }

        public AgendamentoParaListagemDeConsultaViewModel(string idAgendamento, DateTime dataHoraAgendamento, DateTime dataHoraRegistro, string observacoes, MedicoMatSelectViewModel medico, PacienteListarViewModel paciente)
        {
            this.IdAgendamento = idAgendamento;
            this.DataHoraAgendamento = dataHoraAgendamento;
            this.DataHoraRegistro = dataHoraRegistro;
            this.Observacoes = observacoes;
            this.Medico = medico;
            this.Paciente = paciente;
        }

    }
}
