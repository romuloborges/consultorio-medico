using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class MedicoListarViewModel
    {
        public string IdMedico { get; set; }
        public string NomeMedico { get; set; }

        public MedicoListarViewModel()
        {

        }

        public MedicoListarViewModel(string idMedico, string nomeMedico)
        {
            this.IdMedico = idMedico;
            this.NomeMedico = nomeMedico;
        }
    }
}
