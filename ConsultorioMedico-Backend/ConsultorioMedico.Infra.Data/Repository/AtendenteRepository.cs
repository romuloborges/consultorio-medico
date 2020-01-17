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
    public class AtendenteRepository : IAtendenteRepository
    {
        private ConsultorioMedicoContext context;
        public AtendenteRepository(ConsultorioMedicoContext context)
        {
            this.context = context;
        }

        public async Task<bool> CadastrarAtendente(Atendente atendente)
        {
            await this.context.AddAsync<Atendente>(atendente);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<bool> AtualizarAtendente(Atendente atendente)
        {
            this.context.Update<Atendente>(atendente);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<Atendente> BuscarAtendentePorCpf(string cpf)
        {
            var atendente = await this.context.Set<Atendente>().FirstOrDefaultAsync(atendente => atendente.Cpf.Equals(cpf));

            return atendente;
        }

        public async Task<Atendente> BuscarAtendentePorId(Guid id)
        {
            var atendente = await this.context.Set<Atendente>().FirstOrDefaultAsync(atendente => atendente.IdAtendente.Equals(id));

            return atendente;
        }

        public async Task<Atendente> BuscarAtendentePorRg(string rg)
        {
            var atendente = await this.context.Set<Atendente>().FirstOrDefaultAsync(atendente => atendente.Rg.Equals(rg));

            return atendente;
        }

        public async Task<bool> DeletarAtendente(Atendente atendente)
        {
            this.context.Remove<Atendente>(atendente);

            return (await this.context.SaveChangesAsync() > 0);
        }
    }
}
