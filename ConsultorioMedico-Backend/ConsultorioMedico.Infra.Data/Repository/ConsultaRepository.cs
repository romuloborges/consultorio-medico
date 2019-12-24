using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
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

        public IEnumerable<Consulta> BuscarConsultaPorPaciente(Paciente paciente)
        {
            throw new NotImplementedException();
        }

        public bool CadastrarConsulta(Consulta consulta)
        {
            this.context.Add<Consulta>(consulta);

            return (this.context.SaveChanges() > 0);
        }

        public bool DeletarConsulta(Consulta consulta)
        {
            this.context.Remove<Consulta>(consulta);

            return (this.context.SaveChanges() > 0);
        }

        public bool DeletarConsultaPorIdAgendamento(Guid idAgendamento)
        {
            Consulta consulta = this.context.Set<Consulta>().FirstOrDefault(consulta => consulta.IdAgendamento == idAgendamento);
            if(consulta != null)
            {
                this.context.Remove<Consulta>(consulta);
                return (this.context.SaveChanges() > 0);
            }
            return false;
        }

        public IEnumerable<Consulta> ObterTodasConsultas()
        {
            var listaConsulta = this.context.Set<Consulta>().ToList();

            return listaConsulta;
        }
    }
}
