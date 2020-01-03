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

        public bool AtualizarEndereco(Endereco endereco)
        {
            this.context.Update<Endereco>(endereco);

            return (this.context.SaveChanges() > 0);

            //var resultado = (this.context.SaveChanges() > 0);

            //this.context.DetachAllEntities();

            //return resultado;
        }

        public bool CadastrarEndereco(Endereco endereco)
        {
            endereco.IdEndereco = new Guid();
            this.context.Add<Endereco>(endereco);

            return (this.context.SaveChanges() > 0);
        }

        public bool DeletarEndereco(Endereco endereco)
        {
            this.context.Remove<Endereco>(endereco);

            return (this.context.SaveChanges() > 0);
        }

        public Endereco BuscarEnderecoPorId(Guid id)
        {
            var endereco = this.context.Set<Endereco>().AsNoTracking().FirstOrDefault(endereco => endereco.IdEndereco == id);

            return endereco;
        }

        public Guid BuscaIdEndereco(Endereco endereco)
        {
            var e = this.context.Set<Endereco>().AsNoTracking().FirstOrDefault(e => e.Cep.Equals(endereco.Cep) && e.Bairro.Equals(endereco.Bairro) && e.Complemento.Equals(endereco.Complemento)
            && e.Localidade.Equals(endereco.Localidade) && e.Logradouro.Equals(endereco.Logradouro) && e.Numero.Equals(endereco.Numero) && e.Uf.Equals(endereco.Uf));

            this.context.DetachAllEntities();

            if(e != null)
            {
                return e.IdEndereco;
            }

            return Guid.Empty;
        }

        public int QuantidadeReferenciasEndereco(Guid id)
        {
            var lista = this.context.Set<Endereco>().AsNoTracking().Include(endereco => endereco.Atendentes).Include(endereco => endereco.Medicos).Include(endereco => endereco.Pacientes).Where(endereco => endereco.IdEndereco == id).ToList();

            this.context.DetachAllEntities();

            return lista.Count;
        }
    }
}
