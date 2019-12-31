using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.ViewModel.Endereco
{
    public class EnderecoListarEditarViewModel
    {
        public string Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }

        public EnderecoListarEditarViewModel()
        {

        }

        public EnderecoListarEditarViewModel(string id, string cep, string logradouro, string numero, string complemento, string bairro, string localidade, string uf)
        {
            this.Id = id;
            this.Cep = cep;
            this.Logradouro = logradouro;
            this.Numero = numero;
            this.Complemento = complemento;
            this.Bairro = bairro;
            this.Localidade = localidade;
            this.Uf = uf;
        }
    }
}
