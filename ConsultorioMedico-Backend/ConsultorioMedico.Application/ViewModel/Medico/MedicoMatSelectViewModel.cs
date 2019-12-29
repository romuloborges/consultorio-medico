using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class MedicoMatSelectViewModel
    {
        public string IdMedico { get; set; }
        public string NomeMedico { get; set; }

        public MedicoMatSelectViewModel()
        {

        }

        public MedicoMatSelectViewModel(string idMedico, string nomeMedico)
        {
            this.IdMedico = idMedico;
            this.NomeMedico = nomeMedico;
        }
    }
}
