using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ConsultorioMedico.Infra.Data.Repository
{
    public class PacienteRepository : IPacienteRepository
    {
        private ConsultorioMedicoContext context;
        public PacienteRepository(ConsultorioMedicoContext context)
        {
            this.context = context;
        }

        public bool CadastrarPaciente(Paciente paciente)
        {
            this.context.Add<Paciente>(paciente);

            return (this.context.SaveChanges() > 0);
        }
        public bool AtualizarPaciente(Paciente paciente)
        {
            this.context.Update<Paciente>(paciente);

            return (this.context.SaveChanges() > 0);
        }

        public Paciente BuscarPacientePorCpf(string cpf)
        {
            Paciente paciente = this.context.Set<Paciente>().FirstOrDefault(paciente => paciente.Cpf.Equals(cpf));

            return paciente;
        }

        public IEnumerable<Paciente> BuscarPacientePorDataNascimento(DateTime dataNascimento)
        {
            var listaPacientes = this.context.Set<Paciente>().Where(paciente => paciente.DataNascimento.Date == dataNascimento.Date).ToList();

            return listaPacientes;
        }
        public Paciente BuscarPacientePorId(Guid id)
        {
            Paciente paciente = this.context.Set<Paciente>().Include(paciente => paciente.Endereco).FirstOrDefault(paciente => paciente.IdPaciente == id);

            return paciente;
        }

        public IEnumerable<Paciente> BuscarPacientePorNome(string nome)
        {
            var listaPacientes = this.context.Set<Paciente>().Where(paciente => paciente.Nome.Contains(nome));

            return listaPacientes;
        }

        public Paciente BuscarPacientePorRg(string rg)
        {
            Paciente paciente = this.context.Set<Paciente>().FirstOrDefault(paciente => paciente.Rg.Equals(rg));

            return paciente;
        }

        public string ObterNomePaciente(Guid id)
        {
            string nome = this.context.Set<Paciente>().Where(paciente => paciente.IdPaciente == id).Select(paciente => paciente.Nome).ToString();

            return nome;
        }

        public IEnumerable<Paciente> ObterPacientesComFiltro(string nome, string cpf, DateTime dataInicio, DateTime dataFim)
        {
            var lista = this.context.Set<Paciente>().Include(paciente => paciente.Endereco).Where(paciente => nome.Equals("") || paciente.Nome.Contains(nome)).Where(paciente => cpf.Equals("") || paciente.Cpf.Equals(cpf)).Where(paciente => ((dataInicio == DateTime.MinValue && dataFim == DateTime.MinValue) || (dataInicio.Date <= paciente.DataNascimento.Date && paciente.DataNascimento.Date <= dataFim.Date))).ToList();

            return lista;
        }

        public IEnumerable<Paciente> ObterTodosPacientesComEndereco()
        {
            var listaPaciente = this.context.Set<Paciente>().Include(paciente => paciente.Endereco).ToList();

            return listaPaciente;
        }

        public IEnumerable<Paciente> ObterTodosPacientesSemEndereco()
        {
            var listaPaciente = this.context.Set<Paciente>().ToList();

            return listaPaciente;
        }

        public bool DeletarPaciente(Paciente paciente)
        {
            this.context.Remove<Paciente>(paciente);

            return (this.context.SaveChanges() > 0);
        }
    }
}
