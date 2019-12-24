using ConsultorioMedico.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Infra.Data.Configuration
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.HasKey(paciente => paciente.IdPaciente);
            builder.HasIndex(paciente => paciente.Cpf).IsUnique(true);
            builder.HasIndex(paciente => paciente.Rg).IsUnique(true);
            builder.Property(paciente => paciente.Nome).HasMaxLength(100).IsRequired(true);
            builder.Property(paciente => paciente.NomeSocial).HasMaxLength(100).IsRequired(false);
            builder.Property(paciente => paciente.Sexo).HasMaxLength(1).IsRequired(true);
            builder.Property(paciente => paciente.Telefone).HasMaxLength(14).IsRequired(true);
            builder.Property(paciente => paciente.DataNascimento).IsRequired(true);
            builder.Property(paciente => paciente.Email).HasMaxLength(50).IsRequired(true);
            builder.HasOne(paciente => paciente.Endereco).WithMany(endereco => endereco.Pacientes).HasForeignKey(paciente => paciente.IdEndereco).IsRequired(true).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
