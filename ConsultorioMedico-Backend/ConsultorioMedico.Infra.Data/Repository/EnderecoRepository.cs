using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ConsultorioMedico.Infra.Data.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private ConsultorioMedicoContext context;

        public EnderecoRepository(ConsultorioMedicoContext context)
        {
            this.context = context;
        }

        public bool CadastrarEndereco(Endereco endereco)
        {
            endereco.IdEndereco = new Guid();
            this.context.Add<Endereco>(endereco);

            return (this.context.SaveChanges() > 0);
        }

        public bool AtualizarEndereco(Endereco endereco)
        {
            this.context.Update<Endereco>(endereco);

            return (this.context.SaveChanges() > 0);
        }

        public Endereco BuscarEnderecoPorId(Guid id)
        {
            var endereco = this.context.Set<Endereco>().AsNoTracking().FirstOrDefault(endereco => endereco.IdEndereco == id);

            return endereco;
        }

        public Guid BuscaIdEndereco(Endereco endereco)
        {
            var e = this.context.Set<Endereco>().AsNoTracking().FirstOrDefault(e => e.Cep.ToUpper().Equals(endereco.Cep.ToUpper()) && e.Bairro.ToUpper().Equals(endereco.Bairro.ToUpper()) && e.Complemento.ToUpper().Equals(endereco.Complemento.ToUpper())
            && e.Localidade.ToUpper().Equals(endereco.Localidade.ToUpper()) && e.Logradouro.ToUpper().Equals(endereco.Logradouro.ToUpper()) && e.Numero.ToUpper().Equals(endereco.Numero.ToUpper()) && e.Uf.ToUpper().Equals(endereco.Uf.ToUpper()));

            if(e != null)
            {
                return e.IdEndereco;
            }

            return Guid.Empty;
        }

        public int QuantidadeReferenciasEndereco(Guid id)
        {
            var lista = this.context.Set<Endereco>().AsNoTracking().Include(endereco => endereco.Atendentes).Include(endereco => endereco.Medicos).Include(endereco => endereco.Pacientes).Where(endereco => endereco.IdEndereco == id).ToList();

            return lista.Count;
        }

        public bool DeletarEndereco(Endereco endereco)
        {
            this.context.Remove<Endereco>(endereco);

            return (this.context.SaveChanges() > 0);
        }
    }
}
