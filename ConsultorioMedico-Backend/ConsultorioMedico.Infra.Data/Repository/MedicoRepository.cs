using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioMedico.Infra.Data.Repository
{
    public class MedicoRepository : IMedicoRepository
    {
        private ConsultorioMedicoContext context;
        public MedicoRepository(ConsultorioMedicoContext context)
        {
            this.context = context;
        }

        public async Task<bool> CadastrarMedico(Medico medico)
        {
            await this.context.AddAsync<Medico>(medico);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<bool> AtualizarMedico(Medico medico)
        {
            this.context.Update<Medico>(medico);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<Medico> BuscarMedicoPorCpf(string cpf)
        {
            var medico = await this.context.Set<Medico>().FirstOrDefaultAsync(medico => medico.Cpf.Equals(cpf));

            return medico;
        }

        public async Task<Medico> BuscarMedicoPorCrm(int crm)
        {
            var medico = await this.context.Set<Medico>().FirstOrDefaultAsync(medico => medico.Crm == crm);

            return medico;
        }

        public async Task<IEnumerable<Medico>> BuscarMedicoPorNome(string nome)
        {
            var lista = await this.context.Set<Medico>().Where(medico => medico.Nome.Contains(nome)).ToListAsync();

            return lista;
        }

        public async Task<Medico> BuscarMedicoPorRg(string rg)
        {
            var medico = await this.context.Set<Medico>().FirstOrDefaultAsync(medico => medico.Rg.Equals(rg));

            return medico;
        }

        //public async Task<string> ObterNomeMedico(Guid id)
        //{
        //    var nome = this.context.Set<Medico>().Where(medico => medico.IdMedico == id).Select(medico => medico.Nome).ToString();

        //    return nome;
        //}

        public async Task<IEnumerable<Medico>> ObterTodosMedicosComEndereco()
        {
            var listaMedicos = await this.context.Medico.Include(medico => medico.Endereco).ToListAsync();

            return listaMedicos;
        }

        public async Task<IEnumerable<Medico>> ObterTodosMedicosAtivosSemEndereco()
        {
            var listaMedicos = await this.context.Set<Medico>().Where(medico => medico.Ativado).ToListAsync();

            return listaMedicos;
        }

        public async Task<bool> DeletarMedico(Medico medico)
        {
            this.context.Remove<Medico>(medico);

            return (await this.context.SaveChangesAsync() > 0);
        }
    }
}
