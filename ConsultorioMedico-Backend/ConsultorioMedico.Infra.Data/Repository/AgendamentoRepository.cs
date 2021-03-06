﻿using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ConsultorioMedico.Infra.Data.Repository
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private ConsultorioMedicoContext context;

        public AgendamentoRepository(ConsultorioMedicoContext context)
        {
            this.context = context;
        }

        public bool CadastrarAgendamento(Agendamento agendamento)
        {
            agendamento.IdAgendamento = new Guid();
            this.context.Add(agendamento);

            return (this.context.SaveChanges() > 0);
        }

        public bool AtualizarAgendamento(Agendamento agendamento)
        {
            this.context.Update<Agendamento>(agendamento);

            return (this.context.SaveChanges() > 0);
        }

        public IEnumerable<Agendamento> BuscarAgendamentoSemConsultaComFiltro(DateTime dataInicio, DateTime dataFim, Guid idPaciente, Guid idMedico)
        {
            var lista = this.context.Agendamento.AsNoTracking().Include(agendamento => agendamento.Medico).Include(agendamento => agendamento.Paciente).Include(agendamento => agendamento.Consulta).Where(agendamento => ((dataInicio == DateTime.MinValue && dataFim == DateTime.MinValue) || (dataInicio.Date <= agendamento.DataHoraAgendamento.Date && agendamento.DataHoraAgendamento.Date <= dataFim.Date))).Where(agendamento => idPaciente.Equals(Guid.Empty) || agendamento.IdPaciente == idPaciente).Where(agendamento => idMedico.Equals(Guid.Empty) || agendamento.IdMedico == idMedico).Where(agendamento => agendamento.Consulta == null).ToList();

            return lista;
        }

        public IEnumerable<Agendamento> BuscarAgendamentoEntreDataEHora(DateTime dataHoraInicio, DateTime dataHoraFim, Guid idPaciente, Guid idMedico)
        {
            var lista = this.context.Agendamento.AsNoTracking().Include(agendamento => agendamento.Medico).Include(agendamento => agendamento.Paciente).Include(agendamento => agendamento.Consulta).Where(agendamento => (dataHoraInicio <= agendamento.DataHoraAgendamento && agendamento.DataHoraAgendamento <= dataHoraFim)).Where(agendamento => idPaciente.Equals(Guid.Empty) || agendamento.IdPaciente == idPaciente).Where(agendamento => idMedico.Equals(Guid.Empty) || agendamento.IdMedico == idMedico).Where(agendamento => agendamento.Consulta == null).ToList();

            return lista;
        }

        public IEnumerable<Agendamento> BuscarAgendamentoPorDataAgendadaComIdMedico(DateTime dataAgendada, Guid id)
        {
            var listaAgendamentos = this.context.Set<Agendamento>().AsNoTracking().Include(agendamento => agendamento.Medico).Include(agendamento => agendamento.Paciente).Include(agendamento => agendamento.Consulta).Where(agendamento => agendamento.DataHoraAgendamento.Date == dataAgendada.Date).Where(agendamento => id.Equals(Guid.Empty) || agendamento.Medico.IdMedico.Equals(id)).ToList();

            return listaAgendamentos;
        }

        public IEnumerable<Agendamento> BuscarAgendamentoPorDataRegistro(DateTime dataRegistro)
        {
            var listaAgendamento = this.context.Set<Agendamento>().AsNoTracking().Where(agendamento => agendamento.DataHoraRegistro.Date == dataRegistro.Date).ToList();

            return listaAgendamento;
        }

        public Agendamento BuscarAgendamentoPorId(Guid idAgendamento)
        {
            return this.context.Set<Agendamento>().AsNoTracking().FirstOrDefault(agendamento => agendamento.IdAgendamento == idAgendamento);
        }

        public IEnumerable<Agendamento> BuscarAgendamentoPorMedico(Guid idMedico)
        {
            var listaAgendamento = this.context.Set<Agendamento>().Where(agendamento => agendamento.IdMedico == idMedico).ToList();

            return listaAgendamento;
        }

        public IEnumerable<Agendamento> BuscarAgendamentoPorPaciente(Guid idPaciente)
        {
            var listaAgendamento = this.context.Set<Agendamento>().AsNoTracking().Where(agendamento => agendamento.IdPaciente == idPaciente).ToList();

            return listaAgendamento;
        }

        public int ContarAgendamentosPaciente(Guid paciente)
        {
            int quantidade = this.context.Set<Agendamento>().Where(agendamento => agendamento.IdPaciente == paciente).Count();

            return quantidade;
        }

        public bool VerificaExistenciaAgendamentoMedico(Guid idMedico, DateTime dataAgendada)
        {
            Agendamento agendamento = this.context.Set<Agendamento>().FirstOrDefault(agendamento => agendamento.IdMedico == idMedico && agendamento.DataHoraAgendamento == dataAgendada);

            if (agendamento != null)
                return true;
            return false;
        }

        public bool VerificaExistenciaAgendamentoPaciente(Guid idPaciente, DateTime dataAgendada)
        {
            Agendamento agendamento = this.context.Set<Agendamento>().FirstOrDefault(agendamento => agendamento.IdPaciente == idPaciente && agendamento.DataHoraAgendamento == dataAgendada);

            if (agendamento != null)
                return true;
            return false;
        }

        public bool DeletarAgendamento(Agendamento agendamento)
        {
            this.context.Remove<Agendamento>(agendamento);

            return (this.context.SaveChanges() > 0);
        }

        public int QuantidadeAgendamentosMedico(Guid idMedico)
        {
            int quantidade = this.context.Set<Agendamento>().Where(agendamento => agendamento.IdMedico.Equals(idMedico)).Count();

            return quantidade;
        }
    }
}
