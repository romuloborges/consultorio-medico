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

        public IEnumerable<Agendamento> BuscarAgendamentoPorDataAgendada(DateTime dataAgendada)
        {
            var listaAgendamentos = this.context.Agendamento.Include(agendamento => agendamento.Medico).Include(agendamento => agendamento.Paciente).Include(agendamento => agendamento.Consulta).Where(agendamento => agendamento.DataHoraAgendamento == dataAgendada).ToList();

            return listaAgendamentos;
        }

        public IEnumerable<Agendamento> BuscarAgendamentoPorDataRegistro(DateTime dataRegistro)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Agendamento> BuscarAgendamentoPorMedico(Medico medico)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Agendamento> BuscarAgendamentoPorPaciente(Paciente paciente)
        {
            throw new NotImplementedException();
        }

        public bool CadastrarAgendamento(Agendamento agendamento)
        {
            if(!this.VerificaExistenciaAgendamentoMedico(agendamento.IdMedico, agendamento.DataHoraAgendamento) && !this.VerificaExistenciaAgendamentoPaciente(agendamento.IdPaciente, agendamento.DataHoraAgendamento))
            {
                agendamento.IdAgendamento = new Guid();
                this.context.Set<Agendamento>().Add(agendamento);

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
