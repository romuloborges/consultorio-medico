using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Agendamento;
using ConsultorioMedico.Application.ViewModel.Consulta;
using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ConsultorioMedico.Application.Service
{
    public class ConsultaService : IConsultaService
    {
        private IConsultaRepository consultaRepository;
        public ConsultaService(IConsultaRepository consultaRepository)
        {
            this.consultaRepository = consultaRepository;
        }
        public Mensagem AtualizarConsulta(ConsultaComIdAgendamentoViewModel consultaViewModel)
        {
            consultaViewModel.DataHoraTerminoConsulta = TimeZoneInfo.ConvertTime(consultaViewModel.DataHoraTerminoConsulta, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            consultaViewModel.DuracaoConsulta = TimeZoneInfo.ConvertTime(consultaViewModel.DuracaoConsulta, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            if (this.consultaRepository.AtualizarConsulta(new Consulta(new Guid(consultaViewModel.IdConsulta), consultaViewModel.DataHoraTerminoConsulta, consultaViewModel.ReceitaMedica, consultaViewModel.DuracaoConsulta, new Guid(consultaViewModel.IdAgendamento))))
            {
                return new Mensagem(1, "Consulta atualizada com sucesso!");
            }
            return new Mensagem(0, "Falha ao atualizar a consulta!");
        }

        public Mensagem CadastrarConsulta(ConsultaCadastrarViewModel consultaCadastrarViewModel)
        {
            consultaCadastrarViewModel.DataHoraTerminoConsulta = TimeZoneInfo.ConvertTime(consultaCadastrarViewModel.DataHoraTerminoConsulta, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            consultaCadastrarViewModel.DuracaoConsulta = TimeZoneInfo.ConvertTime(consultaCadastrarViewModel.DuracaoConsulta, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            if (this.consultaRepository.CadastrarConsulta(new Consulta(new Guid(), consultaCadastrarViewModel.DataHoraTerminoConsulta, consultaCadastrarViewModel.ReceitaMedica, consultaCadastrarViewModel.DuracaoConsulta, new Guid(consultaCadastrarViewModel.IdAgendamento))))
            {
                return new Mensagem(1, "Consulta cadastrada com sucesso!");
            }
            return new Mensagem(0, "Falha ao cadastrar a consulta!");
        }

        public Mensagem DeletarConsulta(string id)
        {
            var consulta = this.consultaRepository.BuscarConsultaPorId(new Guid(id));

            if(consulta == null)
            {
                return new Mensagem(0, "Esta consulta não existe!");
            }

            bool resultado = this.consultaRepository.DeletarConsulta(consulta);

            if(!resultado)
            {
                return new Mensagem(0, "Não foi possível excluir a consulta!");
            }

            return new Mensagem(1, "Consulta excluída com sucesso!");
        }

        public IEnumerable<ConsultaListarViewModel> ObterConsultasCompletasComFiltro(DateTime dataHoraTerminoConsulta, DateTime dataHoraAgendamento, string idPaciente)
        {
            Guid guidPaciente = idPaciente.Equals("naoha") ? Guid.Empty : new Guid(idPaciente);
            var lista = this.consultaRepository.ObterConsultasCompletasComFiltro(dataHoraTerminoConsulta, dataHoraAgendamento, guidPaciente);
            var listaConsultas = new List<ConsultaListarViewModel>();

            foreach(Consulta consulta in lista)
            {
                MedicoMatSelectViewModel medico = new MedicoMatSelectViewModel(consulta.Agendamento.Medico.IdMedico.ToString(), consulta.Agendamento.Medico.Nome);
                PacienteListarViewModel paciente = new PacienteListarViewModel(consulta.Agendamento.Paciente.IdPaciente.ToString(), consulta.Agendamento.Paciente.Nome, consulta.Agendamento.Paciente.DataNascimento);
                AgendamentoParaListagemDeConsultaViewModel agendamento = new AgendamentoParaListagemDeConsultaViewModel(consulta.Agendamento.IdAgendamento.ToString(), consulta.Agendamento.DataHoraAgendamento, consulta.Agendamento.DataHoraRegistro, consulta.Agendamento.Observacoes, medico, paciente);
                listaConsultas.Add(new ConsultaListarViewModel(consulta.IdConsulta.ToString(), consulta.DataHoraTerminoConsulta, consulta.ReceitaMedica, consulta.DuracaoConsulta, agendamento));
            }

            return listaConsultas;
        }
    }
}
