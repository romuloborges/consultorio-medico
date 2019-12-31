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
        IEnumerable<Paciente> ObterTodosPacientesCompletos();
        IEnumerable<Paciente> BuscarPacientePorNome(string nome);
        IEnumerable<Paciente> BuscarPacientePorDataNascimento(DateTime dataNascimento);
        string ObterNomePaciente(Guid id);
        Paciente BuscarPacientePorId(Guid id);
        Paciente BuscarPacientePorCpf(string cpf);
        Paciente BuscarPacientePorRg(string rg);
        bool DeletarPaciente(Paciente paciente);
    }
}
