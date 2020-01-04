using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsultorioMedico.Infra.Data.Repository
{
    public class ConsultaRepository : IConsultaRepository
    {
        private ConsultorioMedicoContext context;

        public ConsultaRepository(ConsultorioMedicoContext context)
        {
            this.context = context;
        }
<<<<<<< HEAD

        public bool CadastrarConsulta(Consulta consulta)
        {
            this.context.Add<Consulta>(consulta);

            return (this.context.SaveChanges() > 0);
        }

=======
>>>>>>> develop
        public bool AtualizarConsulta(Consulta consulta)
        {
            this.context.Update<Consulta>(consulta);

            return (this.context.SaveChanges() > 0);
        }

        public IEnumerable<Consulta> BuscarConsultaPorData(DateTime dataConsulta)
        {
            var listaConsulta = this.context.Set<Consulta>().Where(consulta => consulta.DataHoraTerminoConsulta.Date == dataConsulta.Date).ToList();

            return listaConsulta;
        }

        public int ContaConsultasPorPaciente(Guid idPaciente)
        {
            int quantidade = this.context.Set<Consulta>().Include(consulta => consulta.Agendamento).Where(consulta => consulta.Agendamento.IdPaciente == idPaciente).Count();

            return quantidade;
        }

<<<<<<< HEAD
        public IEnumerable<Consulta> ObterTodasConsultas()
        {
            var listaConsulta = this.context.Set<Consulta>().ToList();

            return listaConsulta;
        }

        public Consulta BuscarConsultaPorIdAgendamento(Guid idAgendamento)
        {
            var consulta = this.context.Set<Consulta>().FirstOrDefault(consulta => consulta.IdAgendamento == idAgendamento);

            return consulta;
        }

        public IEnumerable<Consulta> ObterConsultasCompletasComFiltro(DateTime dataHoraTerminoConsulta, DateTime dataHoraAgendamento, Guid idPaciente)
        {
            var listaConsultas = this.context.Consulta.Include(consulta => consulta.Agendamento).Include(consulta => consulta.Agendamento.Medico).Include(consulta => consulta.Agendamento.Paciente).Where(consulta => dataHoraTerminoConsulta == DateTime.MinValue || consulta.DataHoraTerminoConsulta.Date == dataHoraTerminoConsulta.Date).Where(consulta => dataHoraAgendamento == DateTime.MinValue || consulta.Agendamento.DataHoraAgendamento.Date == dataHoraAgendamento.Date).Where(consulta => idPaciente == Guid.Empty || consulta.Agendamento.Paciente.IdPaciente == idPaciente).ToList();

            return listaConsultas;
        }

        public Consulta BuscarConsultaPorId(Guid idConsulta)
        {
            var consulta = this.context.Set<Consulta>().AsNoTracking().FirstOrDefault(consulta => consulta.IdConsulta == idConsulta);

            return consulta;
=======
        public bool CadastrarConsulta(Consulta consulta)
        {
            this.context.Add<Consulta>(consulta);

            return (this.context.SaveChanges() > 0);
>>>>>>> develop
        }

        public bool DeletarConsulta(Consulta consulta)
        {
            this.context.Remove<Consulta>(consulta);

            return (this.context.SaveChanges() > 0);
        }

        public bool DeletarConsultaPorIdAgendamento(Guid idAgendamento)
        {
            Consulta consulta = this.context.Set<Consulta>().FirstOrDefault(consulta => consulta.IdAgendamento == idAgendamento);
<<<<<<< HEAD
            if (consulta != null)
=======
            if(consulta != null)
>>>>>>> develop
            {
                this.context.Remove<Consulta>(consulta);
                return (this.context.SaveChanges() > 0);
            }
            return false;
        }
<<<<<<< HEAD
=======

        public IEnumerable<Consulta> ObterTodasConsultas()
        {
            var listaConsulta = this.context.Set<Consulta>().ToList();

            return listaConsulta;
        }

        public Consulta BuscarConsultaPorIdAgendamento(Guid idAgendamento)
        {
            var consulta = this.context.Set<Consulta>().FirstOrDefault(consulta => consulta.IdAgendamento == idAgendamento);

            return consulta;
        }
>>>>>>> develop
    }
}
