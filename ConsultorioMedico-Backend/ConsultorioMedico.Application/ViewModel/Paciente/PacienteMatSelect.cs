using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel
{
    public class PacienteMatSelect
    {
        public string Id { get; set; }
        public string Nome { get; set; }

        public PacienteMatSelect()
        {

        }

        public PacienteMatSelect(string id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }
    }
}
