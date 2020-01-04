using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsultorioMedico.Infra.Data.Repository
{
    public class AtendenteRepository : IAtendenteRepository
    {
        private ConsultorioMedicoContext context;
        public AtendenteRepository(ConsultorioMedicoContext context)
        {
            this.context = context;
        }

        public bool CadastrarAtendente(Atendente atendente)
        {
            this.context.Add<Atendente>(atendente);

            return (this.context.SaveChanges() > 0);
        }

        public bool AtualizarAtendente(Atendente atendente)
        {
            throw new NotImplementedException();
        }

        public Atendente BuscarAtendentePorCpf(string cpf)
        {
            var atendente = this.context.Set<Atendente>().FirstOrDefault(atendente => atendente.Cpf.Equals(cpf));

            return atendente;
        }

        public Atendente BuscarAtendentePorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Atendente BuscarAtendentePorRg(string rg)
        {
            var atendente = this.context.Set<Atendente>().FirstOrDefault(atendente => atendente.Rg.Equals(rg));

            return atendente;
        }

        public bool DeletarAtendente(Atendente atendente)
        {
            this.context.Remove<Atendente>(atendente);

            return (this.context.SaveChanges() > 0);
        }
    }
}
