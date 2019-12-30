using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsultorioMedico.Infra.Data.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private ConsultorioMedicoContext context;

        public EnderecoRepository(ConsultorioMedicoContext context)
        {
            this.context = context;
        }

        public bool AtualizarEndereco(Endereco endereco)
        {
            this.context.Update<Endereco>(endereco);

            return (this.context.SaveChanges() > 0);
        }

        public bool CadastrarEndereco(Endereco endereco)
        {
            this.context.Add<Endereco>(endereco);

            return (this.context.SaveChanges() > 0);
        }

        public bool DeletarEndereco(Endereco endereco)
        {
            this.context.Remove<Endereco>(endereco);

            return (this.context.SaveChanges() > 0);
        }

        public Guid BuscaIdEndereco(Endereco endereco)
        {
            var e = this.context.Set<Endereco>().FirstOrDefault(e => e.Cep.Equals(endereco.Cep) && e.Bairro.Equals(endereco.Bairro) && e.Complemento.Equals(endereco.Complemento)
            && e.Localidade.Equals(endereco.Localidade) && e.Logradouro.Equals(endereco.Logradouro) && e.Numero.Equals(endereco.Numero) && e.Uf.Equals(endereco.Uf));

            if(e != null)
            {
                return e.IdEndereco;
            }

            return Guid.Empty;
        }
    }
}
