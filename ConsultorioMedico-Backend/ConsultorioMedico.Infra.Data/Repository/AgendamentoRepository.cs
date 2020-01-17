using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioMedico.Infra.Data.Repository
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private ConsultorioMedicoContext context;

        public AgendamentoRepository(ConsultorioMedicoContext context)
        {
            this.context = context;
        }

        public async Task<bool> CadastrarAgendamento(Agendamento agendamento)
        {
            agendamento.IdAgendamento = new Guid();
            await this.context.AddAsync(agendamento);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<bool> AtualizarAgendamento(Agendamento agendamento)
        {
            this.context.Update<Agendamento>(agendamento);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<IEnumerable<Agendamento>> BuscarAgendamentoSemConsultaComFiltro(DateTime dataInicio, DateTime dataFim, Guid idPaciente, Guid idMedico)
        {
            var lista = await this.context.Agendamento.AsNoTracking().Include(agendamento => agendamento.Medico).Include(agendamento => agendamento.Paciente).Include(agendamento => agendamento.Consulta).Where(agendamento => ((dataInicio == DateTime.MinValue && dataFim == DateTime.MinValue) || (dataInicio.Date <= agendamento.DataHoraAgendamento.Date && agendamento.DataHoraAgendamento.Date <= dataFim.Date))).Where(agendamento => idPaciente.Equals(Guid.Empty) || agendamento.IdPaciente == idPaciente).Where(agendamento => idMedico.Equals(Guid.Empty) || agendamento.IdMedico == idMedico).Where(agendamento => agendamento.Consulta == null).ToListAsync();

            return lista;
        }

        public async Task<IEnumerable<Agendamento>> BuscarAgendamentoEntreDataEHora(DateTime dataHoraInicio, DateTime dataHoraFim, Guid idPaciente, Guid idMedico)
        {
            var lista = await this.context.Agendamento.AsNoTracking().Include(agendamento => agendamento.Medico).Include(agendamento => agendamento.Paciente).Include(agendamento => agendamento.Consulta).Where(agendamento => (dataHoraInicio <= agendamento.DataHoraAgendamento && agendamento.DataHoraAgendamento <= dataHoraFim)).Where(agendamento => idPaciente.Equals(Guid.Empty) || agendamento.IdPaciente == idPaciente).Where(agendamento => idMedico.Equals(Guid.Empty) || agendamento.IdMedico == idMedico).Where(agendamento => agendamento.Consulta == null).ToListAsync();

            return lista;
        }

        public async Task<IEnumerable<Agendamento>> BuscarAgendamentoPorDataAgendadaComIdMedico(DateTime dataAgendada, Guid id)
        {
            var listaAgendamentos = await this.context.Set<Agendamento>().AsNoTracking().Include(agendamento => agendamento.Medico).Include(agendamento => agendamento.Paciente).Include(agendamento => agendamento.Consulta).Where(agendamento => agendamento.DataHoraAgendamento.Date == dataAgendada.Date).Where(agendamento => id.Equals(Guid.Empty) || agendamento.Medico.IdMedico.Equals(id)).ToListAsync();

            return listaAgendamentos;
        }

        public async Task<IEnumerable<Agendamento>> BuscarAgendamentoPorDataRegistro(DateTime dataRegistro)
        {
            var listaAgendamento = await this.context.Set<Agendamento>().AsNoTracking().Where(agendamento => agendamento.DataHoraRegistro.Date == dataRegistro.Date).ToListAsync();

            return listaAgendamento;
        }

        public async Task<Agendamento> BuscarAgendamentoPorId(Guid idAgendamento)
        {
            return await this.context.Set<Agendamento>().AsNoTracking().FirstOrDefaultAsync(agendamento => agendamento.IdAgendamento == idAgendamento);
        }

        public async Task<IEnumerable<Agendamento>> BuscarAgendamentoPorMedico(Guid idMedico)
        {
            var listaAgendamento = await this.context.Set<Agendamento>().Where(agendamento => agendamento.IdMedico == idMedico).ToListAsync();

            return listaAgendamento;
        }

        public async Task<IEnumerable<Agendamento>> BuscarAgendamentoPorPaciente(Guid idPaciente)
        {
            var listaAgendamento = await this.context.Set<Agendamento>().AsNoTracking().Where(agendamento => agendamento.IdPaciente == idPaciente).ToListAsync();

            return listaAgendamento;
        }

        public async Task<int> ContarAgendamentosPaciente(Guid paciente)
        {
            int quantidade = await this.context.Set<Agendamento>().Where(agendamento => agendamento.IdPaciente == paciente).CountAsync();

            return quantidade;
        }

        public async Task<bool> VerificaExistenciaAgendamentoMedico(Guid idMedico, DateTime dataAgendada)
        {
            Agendamento agendamento = await this.context.Set<Agendamento>().FirstOrDefaultAsync(agendamento => agendamento.IdMedico == idMedico && agendamento.DataHoraAgendamento == dataAgendada);

            if (agendamento != null)
                return true;
            return false;
        }

        public async Task<bool> VerificaExistenciaAgendamentoPaciente(Guid idPaciente, DateTime dataAgendada)
        {
            Agendamento agendamento = await this.context.Set<Agendamento>().FirstOrDefaultAsync(agendamento => agendamento.IdPaciente == idPaciente && agendamento.DataHoraAgendamento == dataAgendada);

            if (agendamento != null)
                return true;
            return false;
        }

        public async Task<bool> DeletarAgendamento(Agendamento agendamento)
        {
            this.context.Remove<Agendamento>(agendamento);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<int> QuantidadeAgendamentosMedico(Guid idMedico)
        {
            int quantidade = await this.context.Set<Agendamento>().Where(agendamento => agendamento.IdMedico.Equals(idMedico)).CountAsync();

            return quantidade;
        }
    }
}
