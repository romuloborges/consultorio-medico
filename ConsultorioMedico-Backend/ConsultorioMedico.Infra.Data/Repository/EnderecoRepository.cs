using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ConsultorioMedico.Infra.Data.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private ConsultorioMedicoContext context;

        public EnderecoRepository(ConsultorioMedicoContext context)
        {
            this.context = context;
        }

        public async Task<bool> CadastrarEndereco(Endereco endereco)
        {
            endereco.IdEndereco = new Guid();
            await this.context.AddAsync<Endereco>(endereco);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<bool> AtualizarEndereco(Endereco endereco)
        {
            this.context.Update<Endereco>(endereco);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<Endereco> BuscarEnderecoPorId(Guid id)
        {
            var endereco = await this.context.Set<Endereco>().AsNoTracking().FirstOrDefaultAsync(endereco => endereco.IdEndereco == id);

            return endereco;
        }

        public async Task<Guid> BuscaIdEndereco(Endereco endereco)
        {
            var e = await this.context.Set<Endereco>().AsNoTracking().FirstOrDefaultAsync(e => e.Cep.ToUpper().Equals(endereco.Cep.ToUpper()) && e.Bairro.ToUpper().Equals(endereco.Bairro.ToUpper()) && e.Complemento.ToUpper().Equals(endereco.Complemento.ToUpper())
            && e.Localidade.ToUpper().Equals(endereco.Localidade.ToUpper()) && e.Logradouro.ToUpper().Equals(endereco.Logradouro.ToUpper()) && e.Numero.ToUpper().Equals(endereco.Numero.ToUpper()) && e.Uf.ToUpper().Equals(endereco.Uf.ToUpper()));

            if(e != null)
            {
                return e.IdEndereco;
            }

            return Guid.Empty;
        }

        public async Task<int> QuantidadeReferenciasEndereco(Guid id)
        {
            var lista = await this.context.Set<Endereco>().AsNoTracking().Include(endereco => endereco.Atendentes).Include(endereco => endereco.Medicos).Include(endereco => endereco.Pacientes).Where(endereco => endereco.IdEndereco == id).ToListAsync();

            return lista.Count;
        }

        public async Task<bool> DeletarEndereco(Endereco endereco)
        {
            this.context.Remove<Endereco>(endereco);

            return (await this.context.SaveChangesAsync() > 0);
        }
    }
}
