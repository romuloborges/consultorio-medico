using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Mensagem AtualizarAgendamento(AgendamentoComIdViewModel agendamentoComIdViewModel)
        {
            var listaAgendamentosRetorno = new List<Agendamento>();
            agendamentoComIdViewModel.DataHoraAgendamento = TimeZoneInfo.ConvertTime(agendamentoComIdViewModel.DataHoraAgendamento, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            agendamentoComIdViewModel.DataHoraRegistro = TimeZoneInfo.ConvertTime(agendamentoComIdViewModel.DataHoraRegistro, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            listaAgendamentosRetorno = this.agendamentoRepository.BuscarAgendamentoEntreDataEHora(agendamentoComIdViewModel.DataHoraAgendamento.Subtract(new TimeSpan(0, 14, 0)), agendamentoComIdViewModel.DataHoraAgendamento.Add(new TimeSpan(0, 14, 0)), Guid.Empty, new Guid(agendamentoComIdViewModel.IdMedico)).ToList();
            if (listaAgendamentosRetorno.Count() > 1 || (listaAgendamentosRetorno.Count == 1 && (listaAgendamentosRetorno.Find(a => a.IdAgendamento.ToString().Equals(agendamentoComIdViewModel.IdAgendamento)) == null)))
            {
                return new Mensagem(0, "Este médico já possui uma consulta marcada neste intervalo de hora!");
            }

            listaAgendamentosRetorno = this.agendamentoRepository.BuscarAgendamentoEntreDataEHora(agendamentoComIdViewModel.DataHoraAgendamento.Subtract(new TimeSpan(0, 14, 0)), agendamentoComIdViewModel.DataHoraAgendamento.Add(new TimeSpan(0, 14, 0)), new Guid(agendamentoComIdViewModel.IdPaciente), Guid.Empty).ToList();
            if (listaAgendamentosRetorno.Count() > 1 || (listaAgendamentosRetorno.Count == 1 && (listaAgendamentosRetorno.Find(a => a.IdAgendamento.ToString().Equals(agendamentoComIdViewModel.IdAgendamento)) == null)))
            {
                return new Mensagem(0, "Este paciente já possui uma consulta marcada neste intervalo de hora!");
            }

            if (this.agendamentoRepository.AtualizarAgendamento(new Agendamento(new Guid(agendamentoComIdViewModel.IdAgendamento), agendamentoComIdViewModel.DataHoraAgendamento, agendamentoComIdViewModel.DataHoraRegistro, agendamentoComIdViewModel.Observacoes, new Guid(agendamentoComIdViewModel.IdMedico), new Guid(agendamentoComIdViewModel.IdPaciente))))
            {
                return new Mensagem(1, "Agendamento atualizado com sucesso!");
            }

            return new Mensagem(0, "Falha ao atualizar o agendamento!");
        }

        public IEnumerable<AgendamentoListarViewModel> BuscarAgendamentoComFiltro(DateTime dataHoraInicio, DateTime dataHoraFim, string idPaciente, string idMedico)
        {
            Guid paciente = idPaciente.Equals("naoha") ? Guid.Empty : new Guid(idPaciente);
            Guid medico = idMedico.Equals("naoha") ? Guid.Empty : new Guid(idMedico);
            var lista = this.agendamentoRepository.BuscarAgendamentoSemConsultaComFiltro(dataHoraInicio, dataHoraFim, paciente, medico);
            var listaAgendamento = new List<AgendamentoListarViewModel>();

            foreach (Agendamento a in lista)
            {
                listaAgendamento.Add(new AgendamentoListarViewModel(a.IdAgendamento.ToString(), a.DataHoraAgendamento, a.DataHoraRegistro, a.Observacoes, new MedicoMatSelectViewModel(a.IdMedico.ToString(), a.Medico.Nome), new PacienteListarViewModel(a.IdPaciente.ToString(), a.Paciente.Nome, a.Paciente.DataNascimento), null));
            }

            return listaAgendamento.OrderBy(agendamento => agendamento.DataHoraAgendamento);
        }

        public IEnumerable<AgendamentoListarViewModel> BuscarAgendamentoPorDataAgendadaComIdMedico(DateTime dataAgendada, string id)
        {
            Guid idParaBusca = id != null ? new Guid(id) : Guid.Empty; 
            var lista = this.agendamentoRepository.BuscarAgendamentoPorDataAgendadaComIdMedico(dataAgendada, idParaBusca);
            var listaAgendamento = new List<AgendamentoListarViewModel>();
            ConsultaViewModel consultaViewModel = null;

            foreach(Agendamento a in lista)
            {
                if(a.Consulta != null)
                {
                    consultaViewModel = new ConsultaViewModel(a.Consulta.IdConsulta.ToString(), a.Consulta.DataHoraTerminoConsulta, a.Consulta.ReceitaMedica, a.Consulta.DataHoraTerminoConsulta);
                } else
                {
                    consultaViewModel = null;
                }
                listaAgendamento.Add(new AgendamentoListarViewModel(a.IdAgendamento.ToString(), a.DataHoraAgendamento, a.DataHoraRegistro, a.Observacoes, new MedicoMatSelectViewModel(a.IdMedico.ToString(), a.Medico.Nome), new PacienteListarViewModel(a.IdPaciente.ToString(), a.Paciente.Nome, a.Paciente.DataNascimento), consultaViewModel));
            }

            return listaAgendamento.OrderBy(agendamento => agendamento.DataHoraAgendamento);
        }

        public Mensagem CadastrarAgendamento(AgendamentoCadastrarViewModel agendamentoViewModel)
        {
            agendamentoViewModel.DataHoraAgendamento = TimeZoneInfo.ConvertTime(agendamentoViewModel.DataHoraAgendamento, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            agendamentoViewModel.DataHoraRegistro = TimeZoneInfo.ConvertTime(agendamentoViewModel.DataHoraRegistro, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            if(this.agendamentoRepository.BuscarAgendamentoEntreDataEHora(agendamentoViewModel.DataHoraAgendamento.Subtract(new TimeSpan(0, 14, 0)), agendamentoViewModel.DataHoraAgendamento.Add(new TimeSpan(0, 14, 0)), Guid.Empty, new Guid(agendamentoViewModel.IdMedico)).ToList().Count() > 0)
            {
                return new Mensagem(0, "Este médico já possui uma consulta marcada neste intervalo de hora!");
            }

            if (this.agendamentoRepository.BuscarAgendamentoEntreDataEHora(agendamentoViewModel.DataHoraAgendamento.Subtract(new TimeSpan(0, 14, 0)), agendamentoViewModel.DataHoraAgendamento.Add(new TimeSpan(0, 14, 0)), new Guid(agendamentoViewModel.IdPaciente), Guid.Empty).ToList().Count() > 0)
            {
                return new Mensagem(0, "Este paciente já possui uma consulta marcada neste intervalo de hora!");
            }

            if (this.agendamentoRepository.CadastrarAgendamento(new Agendamento(agendamentoViewModel.DataHoraAgendamento, agendamentoViewModel.DataHoraRegistro, agendamentoViewModel.Observacoes, new Guid(agendamentoViewModel.IdMedico), new Guid(agendamentoViewModel.IdPaciente))))
            {
                return new Mensagem(1, "Agendamento registrado com sucesso!");
            }

            return new Mensagem(0, "Falha ao registrar agendamento");
        }

        public Mensagem DeletarAgendamento(string id)
        {
            var consulta = this.consultaRepository.BuscarConsultaPorIdAgendamento(new Guid(id));
            if(consulta != null)
            {
                return new Mensagem(0, "Você não pode excluir um agendamento que já teve sua consulta registrada!");
            }

            if (this.agendamentoRepository.DeletarAgendamento(this.agendamentoRepository.BuscarAgendamentoPorId(new Guid(id))))
            {
                return new Mensagem(1, "Agendamento excluído com sucesso!");
            }
            
            return new Mensagem(0, "Falha ao excluir agendamento!");
        }
    }
}
