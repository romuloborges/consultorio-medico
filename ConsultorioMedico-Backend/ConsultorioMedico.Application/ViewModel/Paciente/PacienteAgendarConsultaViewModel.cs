using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class PacienteAgendarConsultaViewModel
    {
        public string IdPaciente { get; set; }
        public string NomePaciente { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public EnderecoViewModel Endereco { get; set; }

        public PacienteAgendarConsultaViewModel()
        {

        }
        public PacienteAgendarConsultaViewModel(string idPaciente, string nomePaciente, DateTime dataNascimento, string cpf, EnderecoViewModel endereco)
        {
            this.IdPaciente = idPaciente;
            this.NomePaciente = nomePaciente;
            this.DataNascimento = dataNascimento;
            this.Cpf = cpf;
            this.Endereco = endereco;
        }
    }
}
