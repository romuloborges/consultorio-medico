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
        public string IdMedico { get; set; }
        public string NomeMedico { get; set; }
        public string IdPaciente { get; set; }
        public string NomePaciente { get; set; }
        public DateTime DataNascimento { get; set; }
        public ConsultaViewModel? ConsultaViewModel { get; set; }

        public AgendamentoListarViewModel()
        {

        }

        public AgendamentoListarViewModel(string idAgendamento, DateTime dataHoraAgendamento, DateTime dataHoraRegistro, string idMedico, string nomeMedico, string idPaciente, string nomePaciente, DateTime dataNascimento, ConsultaViewModel? consultaViewModel)
        {
            this.IdAgendamento = idAgendamento;
            this.DataHoraAgendamento = dataHoraAgendamento;
            this.DataHoraRegistro = dataHoraRegistro;
            this.IdMedico = idMedico;
            this.NomeMedico = nomeMedico;
            this.IdPaciente = idPaciente;
            this.NomePaciente = nomePaciente;
            this.DataNascimento = dataNascimento;
            this.ConsultaViewModel = consultaViewModel;
        }
    }
}
