using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Domain.Entity
{
    public class Endereco
    {
        public Guid IdEndereco { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public List<Medico> Medicos { get; set; }
        public List<Paciente> Pacientes { get; set; }
        public List<Atendente> Atendentes { get; set; }

        public Endereco()
        {

        }
        public Endereco(string cep, string logradouro, string numero, string complemento, string bairro, string localidade, string uf)
        {
            this.Cep = cep;
            this.Logradouro = logradouro;
            this.Numero = numero;
            this.Complemento = complemento;
            this.Bairro = bairro;
            this.Localidade = localidade;
            this.Uf = uf;
        }


        public Endereco(Guid idEndereco, string cep, string logradouro, string numero, string complemento, string bairro, string localidade, string uf, List<Medico> medicos, List<Paciente> pacientes, List<Atendente> atendentes)
        {
            this.IdEndereco = idEndereco;
            this.Cep = cep;
            this.Logradouro = logradouro;
            this.Numero = numero;
            this.Complemento = complemento;
            this.Bairro = bairro;
            this.Localidade = localidade;
            this.Uf = uf;
            this.Medicos = medicos;
            this.Pacientes = pacientes;
            this.Atendentes = atendentes;
        }
    }
}
