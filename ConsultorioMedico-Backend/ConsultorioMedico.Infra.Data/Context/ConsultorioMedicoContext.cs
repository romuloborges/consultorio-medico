using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ConsultorioMedico.Domain.Entity;
using ConsultorioMedico.Infra.Data.Configuration;

namespace ConsultorioMedico.Infra.Data.Context
{
    public class ConsultorioMedicoContext : DbContext
    {
        public ConsultorioMedicoContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Agendamento> Agendamento { get; set; }
        public DbSet<Atendente> Atendente { get; set; }
        public DbSet<Consulta> Consulta { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgendamentoConfiguration());
            modelBuilder.ApplyConfiguration(new AtendenteConfiguration());
            modelBuilder.ApplyConfiguration(new ConsultaConfiguration());
            modelBuilder.ApplyConfiguration(new EnderecoConfiguration());
            modelBuilder.ApplyConfiguration(new MedicoConfiguration());
            modelBuilder.ApplyConfiguration(new PacienteConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
