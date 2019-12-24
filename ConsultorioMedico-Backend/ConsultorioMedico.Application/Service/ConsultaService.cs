using ConsultorioMedico.Application.Service.Interface;
using ConsultorioMedico.Application.ViewModel;
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
        public string AtualizarConsulta(ConsultaCadastrarViewModel consultaCadastrarViewModel)
        {
            if(this.consultaRepository.AtualizarConsulta(new Consulta(new Guid(), consultaCadastrarViewModel.DataHoraTerminoConsulta, consultaCadastrarViewModel.Observacoes, new Guid(consultaCadastrarViewModel.IdAgendamento))))
            {
                return "Consulta atualizada com sucesso!";
            }
            return "Falha ao atualizar a consulta!";
        }

        public string CadastrarConsulta(ConsultaCadastrarViewModel consultaCadastrarViewModel)
        {
            if (this.consultaRepository.CadastrarConsulta(new Consulta(new Guid(), consultaCadastrarViewModel.DataHoraTerminoConsulta, consultaCadastrarViewModel.Observacoes, new Guid(consultaCadastrarViewModel.IdAgendamento))))
            {
                return "Consulta cadastrada com sucesso!";
            }
            return "Falha ao cadastrar a consulta!";
        }

        public string DeletarConsulta(ConsultaViewModel consultaViewModel)
        {
            if (this.consultaRepository.DeletarConsulta(new Consulta(new Guid(consultaViewModel.IdConsulta), consultaViewModel.DataHoraTerminoConsulta, consultaViewModel.Observacoes)))
            {
                return "Consulta excluída com sucesso!";
            }
            return "Falha ao excluir a consulta!";
        }
    }
}
