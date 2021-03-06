﻿using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
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

        public bool CadastrarMedico(Medico medico)
        {
            this.context.Add<Medico>(medico);

            return (this.context.SaveChanges() > 0);
        }

        public bool AtualizarMedico(Medico medico)
        {
            this.context.Update<Medico>(medico);

            return (this.context.SaveChanges() > 0);
        }

        public Medico BuscarMedicoPorCpf(string cpf)
        {
            var medico = this.context.Set<Medico>().FirstOrDefault(medico => medico.Cpf.Equals(cpf));

            return medico;
        }

        public Medico BuscarMedicoPorCrm(int crm)
        {
            var medico = this.context.Set<Medico>().FirstOrDefault(medico => medico.Crm == crm);

            return medico;
        }

        public IEnumerable<Medico> BuscarMedicoPorNome(string nome)
        {
            var lista = this.context.Set<Medico>().Where(medico => medico.Nome.Contains(nome)).ToList();

            return lista;
        }

        public Medico BuscarMedicoPorRg(string rg)
        {
            var medico = this.context.Set<Medico>().FirstOrDefault(medico => medico.Rg.Equals(rg));

            return medico;
        }

        public string ObterNomeMedico(Guid id)
        {
            var nome = this.context.Set<Medico>().Where(medico => medico.IdMedico == id).Select(medico => medico.Nome).ToString();

            return nome;
        }

        public IEnumerable<Medico> ObterTodosMedicosComEndereco()
        {
            var listaMedicos = this.context.Medico.Include(medico => medico.Endereco).ToList();

            return listaMedicos;
        }

        public IEnumerable<Medico> ObterTodosMedicosAtivosSemEndereco()
        {
            var listaMedicos = this.context.Set<Medico>().Where(medico => medico.Ativado).ToList();

            return listaMedicos;
        }

        public bool DeletarMedico(Medico medico)
        {
            this.context.Remove<Medico>(medico);

            return (this.context.SaveChanges() > 0);
        }
    }
}
