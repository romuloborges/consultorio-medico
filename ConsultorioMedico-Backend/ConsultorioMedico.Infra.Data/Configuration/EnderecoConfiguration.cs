using ConsultorioMedico.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsultorioMedico.Infra.Data.Configuration
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(endereco => endereco.IdEndereco);
            builder.Property(endereco => endereco.Cep).HasMaxLength(9).IsRequired(true);
            builder.Property(endereco => endereco.Logradouro).HasMaxLength(100).IsRequired(true);
            builder.Property(endereco => endereco.Complemento).HasMaxLength(50).IsRequired(true);
            builder.Property(endereco => endereco.Bairro).HasMaxLength(50).IsRequired(true);
            builder.Property(endereco => endereco.Localidade).HasMaxLength(100).IsRequired(true);
            builder.Property(endereco => endereco.Uf).HasMaxLength(2).IsRequired(true);
        }
    }
}
