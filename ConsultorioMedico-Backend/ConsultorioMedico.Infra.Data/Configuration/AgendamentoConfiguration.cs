using System;
using System.Collections.Generic;
using System.Text;
using ConsultorioMedico.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsultorioMedico.Infra.Data.Configuration
{
    public class AgendamentoConfiguration : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.HasKey(agendamento => agendamento.IdAgendamento);
            builder.Property(agendamento => agendamento.DataHoraRegistro).IsRequired(true);
            builder.Property(agendamento => agendamento.DataHoraAgendamento).IsRequired(true);
            builder.HasOne(agendamento => agendamento.Medico).WithMany(medico => medico.Agendamentos).HasForeignKey(agendamento => agendamento.IdMedico).IsRequired(true).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(agendamento => agendamento.Paciente).WithMany(paciente => paciente.Agendamentos).HasForeignKey(agendamento => agendamento.IdPaciente).IsRequired(true).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
