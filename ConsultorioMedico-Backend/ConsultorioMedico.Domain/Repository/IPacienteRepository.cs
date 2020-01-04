using System;
using System.Collections.Generic;
using System.Text;
using ConsultorioMedico.Domain.Entity;

namespace ConsultorioMedico.Domain.Repository
{
    public interface IPacienteRepository
    {
        bool CadastrarPaciente(Paciente paciente);
        bool AtualizarPaciente(Paciente paciente);
        IEnumerable<Paciente> ObterTodosPacientesComEndereco();
        IEnumerable<Paciente> ObterTodosPacientesSemEndereco();
        IEnumerable<Paciente> BuscarPacientePorNome(string nome);
        IEnumerable<Paciente> BuscarPacientePorDataNascimento(DateTime dataNascimento);
        Paciente BuscarPacientePorId(Guid id);
        Paciente BuscarPacientePorCpf(string cpf);
        Paciente BuscarPacientePorRg(string rg);
        IEnumerable<Paciente> ObterPacientesComFiltro(string nome, string cpf, DateTime dataInicio, DateTime dataFim);
        string ObterNomePaciente(Guid id);
        bool DeletarPaciente(Paciente paciente);
    }
}
