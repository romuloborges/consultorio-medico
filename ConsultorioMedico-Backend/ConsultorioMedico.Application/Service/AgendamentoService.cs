using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.Service
{
    public class AgendamentoService : IAgendamentoService
    {
        private IAgendamentoRepository agendamentoRepository;
        private IConsultaRepository consultaRepository;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository, IConsultaRepository consultaRepository)
        {
            this.agendamentoRepository = agendamentoRepository;
            this.consultaRepository = consultaRepository;
        }

        public string AtualizarAgendamento(AgendamentoComIdViewModel agendamentoComIdViewModel)
        {
            if (this.agendamentoRepository.AtualizarAgendamento(new Agendamento(new Guid(agendamentoComIdViewModel.IdAgendamento), agendamentoComIdViewModel.DataHoraAgendamento, agendamentoComIdViewModel.DataHoraRegistro, new Guid(agendamentoComIdViewModel.IdMedico), new Guid(agendamentoComIdViewModel.IdPaciente))))
            {
                return "Agendamento atualizado com sucesso!";
            }
            return "Falha ao atualizar o agendamento!";
        }

        public IEnumerable<AgendamentoListarViewModel> BuscarAgendamentoPorDataAgendada(DateTime dataAgendada)
        {
            var lista = this.agendamentoRepository.BuscarAgendamentoPorDataAgendada(dataAgendada);
            var listaAgendamento = new List<AgendamentoListarViewModel>();
            ConsultaViewModel consultaViewModel = null;

            foreach(Agendamento a in lista)
            {
                if(a.Consulta != null)
                {
                    consultaViewModel = new ConsultaViewModel(a.Consulta.IdConsulta.ToString(), a.Consulta.DataHoraTerminoConsulta, a.Consulta.Observacoes);
                } else
                {
                    consultaViewModel = null;
                }
                listaAgendamento.Add(new AgendamentoListarViewModel(a.IdAgendamento.ToString(), a.DataHoraAgendamento, a.DataHoraRegistro, new MedicoMatSelectViewModel(a.IdMedico.ToString(), a.Medico.Nome), new PacienteListarViewModel(a.IdPaciente.ToString(), a.Paciente.Nome, a.Paciente.DataNascimento), consultaViewModel));
            }

            return listaAgendamento;
        }

        public string CadastrarAgendamento(AgendamentoViewModel agendamentoViewModel)
        {
            if(this.agendamentoRepository.CadastrarAgendamento(new Agendamento(agendamentoViewModel.DataHoraAgendamento, agendamentoViewModel.DataHoraRegistro, agendamentoViewModel.IdMedico, agendamentoViewModel.IdPaciente)))
            {
                return "Agendamento registrado com sucesso!";
            }
            return "Falha ao registrar agendamento";
        }

        //public string DeletarAgendamento(AgendamentoComIdViewModel agendamentoComIdViewModel)
        //{
        //    // Excluir a consulta do agendamento
        //    if(this.agendamentoRepository.DeletarAgendamento(new Agendamento(new Guid(agendamentoComIdViewModel.IdAgendamento), agendamentoComIdViewModel.DataHoraAgendamento, agendamentoComIdViewModel.DataHoraRegistro, new Guid(agendamentoComIdViewModel.IdMedico), new Guid(agendamentoComIdViewModel.IdPaciente))))
        //    {
        //        return "Agendamento excluído com sucesso!";
        //    }
        //    return "Falha ao excluir agendamento!";
        //}
        public string DeletarAgendamento(string id)
        {
            if(this.consultaRepository.DeletarConsultaPorIdAgendamento(new Guid(id)) && this.agendamentoRepository.DeletarAgendamento(this.agendamentoRepository.BuscarAgendamentoPorId(new Guid(id))))
            {
                return "Agendamento excluído com sucesso!";
            }
            return "Falha ao excluir agendamento!";
        }
    }
}
