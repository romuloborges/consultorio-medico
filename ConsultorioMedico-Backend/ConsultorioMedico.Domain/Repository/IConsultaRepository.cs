using System;
using System.Collections.Generic;
using System.Text;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IConsultaRepository
    {
        bool CadastrarConsulta(Consulta consulta);
        bool AtualizarConsulta(Consulta consulta);
        IEnumerable<Consulta> ObterTodasConsultas();
        IEnumerable<Consulta> BuscarConsultaPorData(DateTime dataConsulta);
        IEnumerable<Consulta> BuscarConsultaPorPaciente(Paciente paciente);
        bool DeletarConsulta(Consulta consulta);
    }
}
