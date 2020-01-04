using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
>>>>>>> develop
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
<<<<<<< HEAD

        public bool CadastrarMedico(Medico medico)
        {
            this.context.Add<Medico>(medico);

            return (this.context.SaveChanges() > 0);
        }

        public bool AtualizarMedico(Medico medico)
        {
            this.context.Update<Medico>(medico);

            return (this.context.SaveChanges() > 0);
=======
        public bool AtualizarMedico(Medico medico)
        {
            throw new NotImplementedException();
>>>>>>> develop
        }

        public Medico BuscarMedicoPorCpf(string cpf)
        {
<<<<<<< HEAD
            var medico = this.context.Set<Medico>().FirstOrDefault(medico => medico.Cpf.Equals(cpf));

            return medico;
=======
            throw new NotImplementedException();
>>>>>>> develop
        }

        public Medico BuscarMedicoPorCrm(int crm)
        {
<<<<<<< HEAD
            var medico = this.context.Set<Medico>().FirstOrDefault(medico => medico.Crm == crm);

            return medico;
=======
            throw new NotImplementedException();
>>>>>>> develop
        }

        public IEnumerable<Medico> BuscarMedicoPorNome(string nome)
        {
<<<<<<< HEAD
            var lista = this.context.Set<Medico>().Where(medico => medico.Nome.Contains(nome)).ToList();

            return lista;
        }

        public Medico BuscarMedicoPorRg(string rg)
        {
            var medico = this.context.Set<Medico>().FirstOrDefault(medico => medico.Rg.Equals(rg));

            return medico;
=======
            throw new NotImplementedException();
        }

        public bool CadastrarMedico(Medico medico)
        {
            throw new NotImplementedException();
        }

        public bool DeletarMedico(Medico medico)
        {
            throw new NotImplementedException();
>>>>>>> develop
        }

        public string ObterNomeMedico(Guid id)
        {
<<<<<<< HEAD
            var nome = this.context.Set<Medico>().Where(medico => medico.IdMedico == id).Select(medico => medico.Nome).ToString();

            return nome;
=======
            throw new NotImplementedException();
>>>>>>> develop
        }

        public IEnumerable<Medico> ObterTodosMedicosComEndereco()
        {
<<<<<<< HEAD
            var listaMedicos = this.context.Medico.Include(medico => medico.Endereco).ToList();

            return listaMedicos;
=======
            throw new NotImplementedException();
>>>>>>> develop
        }

        public IEnumerable<Medico> ObterTodosMedicosSemEndereco()
        {
            var listaMedicos = this.context.Set<Medico>().ToList();

            return listaMedicos;
        }
<<<<<<< HEAD

        public bool DeletarMedico(Medico medico)
        {
            this.context.Remove<Medico>(medico);

            return (this.context.SaveChanges() > 0);
        }
=======
>>>>>>> develop
    }
}
