using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application
{
    public class Mensagem
    {
        public int Id { get; set; }
        public string Texto { get; set; }

        public Mensagem()
        {

        }

        public Mensagem(int id, string texto)
        {
            this.Id = id;
            this.Texto = texto;
        }
    }
}
