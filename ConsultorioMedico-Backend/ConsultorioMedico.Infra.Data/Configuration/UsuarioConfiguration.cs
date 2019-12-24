using ConsultorioMedico.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Infra.Data.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(usuario => usuario.IdUsuario);
            builder.HasIndex(usuario => usuario.Email).IsUnique(true);
            builder.Property(usuario => usuario.Senha).HasMaxLength(25).IsRequired(true);
            builder.Property(usuario => usuario.Tipo).HasMaxLength(50).IsRequired(true);
        }
    }
}
