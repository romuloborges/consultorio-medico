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
    public class ConsultaRepository : IConsultaRepository
    {
        private ConsultorioMedicoContext context;

        public ConsultaRepository(ConsultorioMedicoContext context)
        {
            this.context = context;
        }

        public async Task<bool> CadastrarConsulta(Consulta consulta)
        {
            await this.context.AddAsync<Consulta>(consulta);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<bool> AtualizarConsulta(Consulta consulta)
        {
            this.context.Update<Consulta>(consulta);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<IEnumerable<Consulta>> BuscarConsultaPorData(DateTime dataConsulta)
        {
            var listaConsulta = await this.context.Set<Consulta>().Where(consulta => consulta.DataHoraTerminoConsulta.Date == dataConsulta.Date).ToListAsync();

            return listaConsulta;
        }

        public async Task<int> ContaConsultasPorPaciente(Guid idPaciente)
        {
            int quantidade = await this.context.Set<Consulta>().Include(consulta => consulta.Agendamento).Where(consulta => consulta.Agendamento.IdPaciente == idPaciente).CountAsync();

            return quantidade;
        }

        public async Task<IEnumerable<Consulta>> ObterTodasConsultas()
        {
            var listaConsulta = await this.context.Set<Consulta>().ToListAsync();

            return listaConsulta;
        }

        public async Task<Consulta> BuscarConsultaPorIdAgendamento(Guid idAgendamento)
        {
            var consulta = await this.context.Set<Consulta>().FirstOrDefaultAsync(consulta => consulta.IdAgendamento == idAgendamento);

            return consulta;
        }

        public async Task<IEnumerable<Consulta>> ObterConsultasCompletasComFiltro(DateTime dataHoraTerminoConsulta, DateTime dataHoraAgendamento, Guid idPaciente)
        {
            var listaConsultas = await this.context.Consulta.Include(consulta => consulta.Agendamento).Include(consulta => consulta.Agendamento.Medico).Include(consulta => consulta.Agendamento.Paciente).Where(consulta => dataHoraTerminoConsulta == DateTime.MinValue || consulta.DataHoraTerminoConsulta.Date == dataHoraTerminoConsulta.Date).Where(consulta => dataHoraAgendamento == DateTime.MinValue || consulta.Agendamento.DataHoraAgendamento.Date == dataHoraAgendamento.Date).Where(consulta => idPaciente == Guid.Empty || consulta.Agendamento.Paciente.IdPaciente == idPaciente).ToListAsync();

            return listaConsultas;
        }

        public async Task<Consulta> BuscarConsultaPorId(Guid idConsulta)
        {
            var consulta = await this.context.Set<Consulta>().AsNoTracking().FirstOrDefaultAsync(consulta => consulta.IdConsulta == idConsulta);

            return consulta;
        }

        public async Task<bool> DeletarConsulta(Consulta consulta)
        {
            this.context.Remove<Consulta>(consulta);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<bool> DeletarConsultaPorIdAgendamento(Guid idAgendamento)
        {
            Consulta consulta = await this.context.Set<Consulta>().FirstOrDefaultAsync(consulta => consulta.IdAgendamento == idAgendamento);
            if (consulta != null)
            {
                this.context.Remove<Consulta>(consulta);
                return (await this.context.SaveChangesAsync() > 0);
            }
            return false;
        }
    }
}
