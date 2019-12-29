using ConsultorioMedico.Domain.Entity;
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
        public bool AtualizarAgendamento(Agendamento agendamento)
        {
            this.context.Update<Agendamento>(agendamento);

            return (this.context.SaveChanges() > 0);
        }

        public IEnumerable<Agendamento> BuscarAgendamentoComFiltro(DateTime? dataHoraInicio, DateTime? dataHoraFim, Guid? idPaciente, Guid? idMedico)
        {
            var lista = this.context.Agendamento.Include(agendamento => agendamento.Medico).Include(agendamento => agendamento.Paciente).Include(agendamento => agendamento.Consulta).Where(agendamento => ((dataHoraInicio == DateTime.MinValue && dataHoraFim == DateTime.MinValue) || (dataHoraInicio.Value.Date >= agendamento.DataHoraAgendamento.Date && agendamento.DataHoraAgendamento.Date <= dataHoraFim.Value.Date))).Where(agendamento => idPaciente.Value.Equals(Guid.Empty) || agendamento.IdPaciente == idPaciente).Where(agendamento => idMedico.Value.Equals(Guid.Empty) || agendamento.IdMedico == idMedico).ToList();

            return lista;
        }

        public IEnumerable<Agendamento> BuscarAgendamentoPorDataAgendada(DateTime dataAgendada)
        {
            var listaAgendamentos = this.context.Set<Agendamento>().Include(agendamento => agendamento.Medico).Include(agendamento => agendamento.Paciente).Include(agendamento => agendamento.Consulta).Where(agendamento => agendamento.DataHoraAgendamento.Date == dataAgendada.Date/* == dataAgendada*/).ToList();

            return listaAgendamentos;
        }

        public IEnumerable<Agendamento> BuscarAgendamentoPorDataRegistro(DateTime dataRegistro)
        {
            var listaAgendamento = this.context.Set<Agendamento>().Where(agendamento => agendamento.DataHoraRegistro.Date == dataRegistro.Date).ToList();

            return listaAgendamento;
        }

        public Agendamento BuscarAgendamentoPorId(Guid idAgendamento)
        {
            return this.context.Set<Agendamento>().FirstOrDefault(agendamento => agendamento.IdAgendamento == idAgendamento);
        }

        public IEnumerable<Agendamento> BuscarAgendamentoPorMedico(Guid idMedico)
        {
            var listaAgendamento = this.context.Set<Agendamento>().Where(agendamento => agendamento.IdMedico == idMedico).ToList();

            return listaAgendamento;
        }

        public IEnumerable<Agendamento> BuscarAgendamentoPorPaciente(Guid idPaciente)
        {
            var listaAgendamento = this.context.Set<Agendamento>().Where(agendamento => agendamento.IdPaciente == idPaciente).ToList();

            return listaAgendamento;
        }

        public bool CadastrarAgendamento(Agendamento agendamento)
        {
            if(!this.VerificaExistenciaAgendamentoMedico(agendamento.IdMedico, agendamento.DataHoraAgendamento) && !this.VerificaExistenciaAgendamentoPaciente(agendamento.IdPaciente, agendamento.DataHoraAgendamento))
            {
                agendamento.IdAgendamento = new Guid();
                this.context.Add(agendamento);

                return (this.context.SaveChanges() > 0);
            }
            return false;
        }

        public bool DeletarAgendamento(Agendamento agendamento)
        {
            this.context.Remove<Agendamento>(agendamento);

            return (this.context.SaveChanges() > 0);
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
    }
}
