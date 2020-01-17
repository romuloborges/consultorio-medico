using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Domain.Repository;
using ConsultorioMedico.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace ConsultorioMedico.Infra.Data.Repository
{
    public class PacienteRepository : IPacienteRepository
    {
        private ConsultorioMedicoContext context;
        public PacienteRepository(ConsultorioMedicoContext context)
        {
            this.context = context;
        }

        public async Task<bool> CadastrarPaciente(Paciente paciente)
        {
            await this.context.AddAsync<Paciente>(paciente);

            return (await this.context.SaveChangesAsync() > 0);
        }
        public async Task<bool> AtualizarPaciente(Paciente paciente)
        {
            this.context.Update<Paciente>(paciente);

            return (await this.context.SaveChangesAsync() > 0);
        }

        public async Task<Paciente> BuscarPacientePorCpf(string cpf)
        {
            Paciente paciente = await this.context.Set<Paciente>().AsNoTracking().FirstOrDefaultAsync(paciente => paciente.Cpf.Equals(cpf));

            return paciente;
        }

        public async Task<IEnumerable<Paciente>> BuscarPacientePorDataNascimento(DateTime dataNascimento)
        {
            var listaPacientes = await this.context.Set<Paciente>().Where(paciente => paciente.DataNascimento.Date == dataNascimento.Date).ToListAsync();

            return listaPacientes;
        }
        public async Task<Paciente> BuscarPacientePorId(Guid id)
        {
            Paciente paciente = await this.context.Set<Paciente>().Include(paciente => paciente.Endereco).FirstOrDefaultAsync(paciente => paciente.IdPaciente == id);

            return paciente;
        }

        public async Task<IEnumerable<Paciente>> BuscarPacientePorNome(string nome)
        {
            var listaPacientes = await this.context.Set<Paciente>().Where(paciente => paciente.Nome.Contains(nome)).ToListAsync();

            return listaPacientes;
        }

        public async Task<Paciente> BuscarPacientePorRg(string rg)
        {
            Paciente paciente = await this.context.Set<Paciente>().AsNoTracking().FirstOrDefaultAsync(paciente => paciente.Rg.Equals(rg));

            return paciente;
        }

        //public string ObterNomePaciente(Guid id)
        //{
        //    string nome = this.context.Set<Paciente>().Where(paciente => paciente.IdPaciente == id).Select(paciente => paciente.Nome).ToString();

        //    return nome;
        //}

        public async Task<IEnumerable<Paciente>> ObterPacientesComFiltro(string nome, string cpf, DateTime dataInicio, DateTime dataFim)
        {
            var lista = await this.context.Set<Paciente>().Include(paciente => paciente.Endereco).Where(paciente => nome.Equals("") || paciente.Nome.Contains(nome)).Where(paciente => cpf.Equals("") || paciente.Cpf.Equals(cpf)).Where(paciente => ((dataInicio == DateTime.MinValue && dataFim == DateTime.MinValue) || (dataInicio.Date <= paciente.DataNascimento.Date && paciente.DataNascimento.Date <= dataFim.Date))).ToListAsync();

            return lista;
        }

        public async Task<IEnumerable<Paciente>> ObterTodosPacientesComEndereco()
        {
            var listaPaciente = await this.context.Set<Paciente>().Include(paciente => paciente.Endereco).ToListAsync();

            return listaPaciente;
        }

        public async Task<IEnumerable<Paciente>> ObterTodosPacientesSemEndereco()
        {
            var listaPaciente = await this.context.Set<Paciente>().ToListAsync();

            return listaPaciente;
        }

        public async Task<bool> DeletarPaciente(Paciente paciente)
        {
            this.context.Remove<Paciente>(paciente);

            return (await this.context.SaveChangesAsync() > 0);
        }
    }
}
