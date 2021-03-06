﻿using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Paciente;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IPacienteService
    {
        Mensagem CadastrarPaciente(PacienteCadastrarViewModel pacienteCadastrarViewModel);
        PacienteAgendarConsultaViewModel ObterPacienteConsulta(string id);
        PacienteListarEditarViewModel ObterPacienteCompleto(string id);
        PacienteCadastrarViewModel ObterPacienteParaRegistrarConsulta(string id);
        IEnumerable<PacienteMatSelect> ObterTodosPacientesParaMatSelect();
        IEnumerable<PacienteTabelaListarViewModel> ObterTodosPacientesParaTabela();
        IEnumerable<PacienteTabelaListarViewModel> ObterPacientesComFiltroParaTabela(string nome, string cpf, DateTime dataInicio, DateTime dataFim);
        Mensagem AtualizarPaciente(PacienteListarEditarViewModel pacienteListarEditarViewModel);
        Mensagem DeletarPaciente(string id);
    }
}
