using ConsultorioMedico.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Infra.Data.Configuration
{
    public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.HasKey(medico => medico.IdMedico);
            builder.HasIndex(medico => medico.Cpf).IsUnique(true);
            builder.HasIndex(medico => medico.Rg).IsUnique(true);
            builder.HasIndex(medico => medico.Crm).IsUnique(true);
            builder.Property(medico => medico.Nome).HasMaxLength(100).IsRequired(true);
            builder.Property(medico => medico.Sexo).HasMaxLength(1).IsRequired(true);
            builder.Property(medico => medico.Telefone).HasMaxLength(14).IsRequired(true);
            builder.Property(medico => medico.DataNascimento).IsRequired(true);
            builder.Property(medico => medico.Email).HasMaxLength(50).IsRequired(true);
            builder.HasOne(medico => medico.Endereco).WithMany(endereco => endereco.Medicos).HasForeignKey(medico => medico.IdEndereco).IsRequired(true).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(medico => medico.Usuario).WithOne(usuario => usuario.Medico).HasForeignKey<Usuario>(usuario => usuario.IdMedico).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
