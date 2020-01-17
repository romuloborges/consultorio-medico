using ConsultorioMedico.Application.ViewModel;
using ConsultorioMedico.Application.ViewModel.Paciente;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioMedico.Application.Service.Interface
{
    public interface IPacienteService
    {
        Task<Mensagem> CadastrarPaciente(PacienteCadastrarViewModel pacienteCadastrarViewModel);
        Task<PacienteAgendarConsultaViewModel> ObterPacienteConsulta(string id);
        Task<PacienteListarEditarViewModel> ObterPacienteCompleto(string id);
        Task<PacienteCadastrarViewModel> ObterPacienteParaRegistrarConsulta(string id);
        Task<IEnumerable<PacienteMatSelect>> ObterTodosPacientesParaMatSelect();
        Task<IEnumerable<PacienteTabelaListarViewModel>> ObterTodosPacientesParaTabela();
        Task<IEnumerable<PacienteTabelaListarViewModel>> ObterPacientesComFiltroParaTabela(string nome, string cpf, DateTime dataInicio, DateTime dataFim);
        Task<Mensagem> AtualizarPaciente(PacienteListarEditarViewModel pacienteListarEditarViewModel);
        Task<Mensagem> DeletarPaciente(string id);
    }
}
