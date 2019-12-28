using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class PacienteListarViewModel
    {
        public string IdPaciente { get; set; }
        public string NomePaciente { get; set; }
        public DateTime DataNascimento { get; set; }

        public PacienteListarViewModel()
        {

        }
        public PacienteListarViewModel(string idPaciente, string nomePaciente, DateTime dataNascimento)
        {
            this.IdPaciente = idPaciente;
            this.NomePaciente = nomePaciente;
            this.DataNascimento = dataNascimento;
        }
    }
}
