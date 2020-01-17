using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IPacienteRepository
    {
        Task<bool> CadastrarPaciente(Paciente paciente);
        Task<bool> AtualizarPaciente(Paciente paciente);
        Task<IEnumerable<Paciente>> ObterTodosPacientesComEndereco();
        Task<IEnumerable<Paciente>> ObterTodosPacientesSemEndereco();
        Task<IEnumerable<Paciente>> BuscarPacientePorNome(string nome);
        Task<IEnumerable<Paciente>> BuscarPacientePorDataNascimento(DateTime dataNascimento);
        Task<Paciente> BuscarPacientePorId(Guid id);
        Task<Paciente> BuscarPacientePorCpf(string cpf);
        Task<Paciente> BuscarPacientePorRg(string rg);
        Task<IEnumerable<Paciente>> ObterPacientesComFiltro(string nome, string cpf, DateTime dataInicio, DateTime dataFim);
        Task<bool> DeletarPaciente(Paciente paciente);
    }
}
