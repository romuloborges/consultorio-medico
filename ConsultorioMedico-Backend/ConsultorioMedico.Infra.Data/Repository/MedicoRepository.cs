using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsultorioMedico.Infra.Data.Repository
{
    public class MedicoRepository : IMedicoRepository
    {
        private ConsultorioMedicoContext context;
        public MedicoRepository(ConsultorioMedicoContext context)
        {
            this.context = context;
        }
        public bool AtualizarMedico(Medico medico)
        {
            throw new NotImplementedException();
        }

        public Medico BuscarMedicoPorCpf(string cpf)
        {
            throw new NotImplementedException();
        }

        public Medico BuscarMedicoPorCrm(int crm)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Medico> BuscarMedicoPorNome(string nome)
        {
            throw new NotImplementedException();
        }

        public bool CadastrarMedico(Medico medico)
        {
            throw new NotImplementedException();
        }

        public bool DeletarMedico(Medico medico)
        {
            throw new NotImplementedException();
        }

        public string ObterNomeMedico(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Medico> ObterTodosMedicosComEndereco()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Medico> ObterTodosMedicosSemEndereco()
        {
            var listaMedicos = this.context.Set<Medico>().ToList();

            return listaMedicos;
        }
    }
}
