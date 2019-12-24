using System;
using System.Collections.Generic;
using System.Text;
using ConsultorioMedico.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsultorioMedico.Infra.Data.Configuration
{
    public class AtendenteConfiguration : IEntityTypeConfiguration<Atendente>
    {
        public void Configure(EntityTypeBuilder<Atendente> builder)
        {
            builder.HasKey(atendente => atendente.IdAtendente);
            builder.HasIndex(atendente => atendente.Cpf).IsUnique(true);
            builder.HasIndex(atendente => atendente.Rg).IsUnique(true);
            builder.Property(atendente => atendente.Nome).HasMaxLength(100).IsRequired(true);
            builder.Property(atendente => atendente.Sexo).HasMaxLength(1).IsRequired(true);
            builder.Property(atendente => atendente.Telefone).HasMaxLength(14).IsRequired(true);
            builder.Property(atendente => atendente.DataNascimento).IsRequired(true);
            builder.Property(atendente => atendente.Email).HasMaxLength(50).IsRequired(true);
            builder.HasOne(atendente => atendente.Endereco).WithMany(endereco => endereco.Atendentes).HasForeignKey(atendente => atendente.IdEndereco).IsRequired(true).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(atendente => atendente.Usuario).WithOne(usuario => usuario.Atendente).HasForeignKey<Usuario>(usuario => usuario.IdAtendente).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
