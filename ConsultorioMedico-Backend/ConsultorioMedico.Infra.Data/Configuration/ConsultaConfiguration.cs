﻿using ConsultorioMedico.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsultorioMedico.Infra.Data.Configuration
{
    public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.HasKey(consulta => consulta.IdConsulta);
            builder.Property(consulta => consulta.DataHoraTerminoConsulta).IsRequired(true);
            builder.Property(consulta => consulta.ReceitaMedica).HasMaxLength(2000).IsRequired(true);
            builder.Property(consulta => consulta.DuracaoConsulta).IsRequired(true);
            builder.HasOne(consulta => consulta.Agendamento).WithOne(agendamento => agendamento.Consulta).HasForeignKey<Consulta>(consulta => consulta.IdAgendamento).IsRequired(true);
        }
    }
}
