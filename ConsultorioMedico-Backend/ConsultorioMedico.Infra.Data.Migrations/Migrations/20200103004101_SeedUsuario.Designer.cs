﻿// <auto-generated />
using System;
using ConsultorioMedico.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConsultorioMedico.Infra.Data.Migrations.Migrations
{
    [DbContext(typeof(ConsultorioMedicoContext))]
    [Migration("20200103004101_SeedUsuario")]
    partial class SeedUsuario
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ConsultorioMedico.Domain.Entity.Agendamento", b =>
                {
                    b.Property<Guid>("IdAgendamento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataHoraAgendamento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataHoraRegistro")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdMedico")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdPaciente")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("IdAgendamento");

                    b.HasIndex("IdMedico");

                    b.HasIndex("IdPaciente");

                    b.ToTable("Agendamento");
                });

            modelBuilder.Entity("ConsultorioMedico.Domain.Entity.Atendente", b =>
                {
                    b.Property<Guid>("IdAtendente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("IdEndereco")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Rg")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(14)")
                        .HasMaxLength(14);

                    b.HasKey("IdAtendente");

                    b.HasIndex("Cpf")
                        .IsUnique()
                        .HasFilter("[Cpf] IS NOT NULL");

                    b.HasIndex("IdEndereco");

                    b.HasIndex("Rg")
                        .IsUnique()
                        .HasFilter("[Rg] IS NOT NULL");

                    b.ToTable("Atendente");
                });

            modelBuilder.Entity("ConsultorioMedico.Domain.Entity.Consulta", b =>
                {
                    b.Property<Guid>("IdConsulta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataHoraTerminoConsulta")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdAgendamento")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ReceitaMedica")
                        .IsRequired()
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.HasKey("IdConsulta");

                    b.HasIndex("IdAgendamento")
                        .IsUnique();

                    b.ToTable("Consulta");
                });

            modelBuilder.Entity("ConsultorioMedico.Domain.Entity.Endereco", b =>
                {
                    b.Property<Guid>("IdEndereco")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("nvarchar(9)")
                        .HasMaxLength(9);

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Localidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<string>("Uf")
                        .IsRequired()
                        .HasColumnType("nvarchar(2)")
                        .HasMaxLength(2);

                    b.HasKey("IdEndereco");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("ConsultorioMedico.Domain.Entity.Medico", b =>
                {
                    b.Property<Guid>("IdMedico")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Crm")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("IdEndereco")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Rg")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(14)")
                        .HasMaxLength(14);

                    b.HasKey("IdMedico");

                    b.HasIndex("Cpf")
                        .IsUnique()
                        .HasFilter("[Cpf] IS NOT NULL");

                    b.HasIndex("Crm")
                        .IsUnique();

                    b.HasIndex("IdEndereco");

                    b.HasIndex("Rg")
                        .IsUnique()
                        .HasFilter("[Rg] IS NOT NULL");

                    b.ToTable("Medico");
                });

            modelBuilder.Entity("ConsultorioMedico.Domain.Entity.Paciente", b =>
                {
                    b.Property<Guid>("IdPaciente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<Guid>("IdEndereco")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("NomeSocial")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Rg")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(14)")
                        .HasMaxLength(14);

                    b.HasKey("IdPaciente");

                    b.HasIndex("Cpf")
                        .IsUnique()
                        .HasFilter("[Cpf] IS NOT NULL");

                    b.HasIndex("IdEndereco");

                    b.HasIndex("Rg")
                        .IsUnique()
                        .HasFilter("[Rg] IS NOT NULL");

                    b.ToTable("Paciente");
                });

            modelBuilder.Entity("ConsultorioMedico.Domain.Entity.Usuario", b =>
                {
                    b.Property<Guid>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("IdAtendente")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IdMedico")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(65)")
                        .HasMaxLength(65);

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("IdUsuario");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("IdAtendente")
                        .IsUnique()
                        .HasFilter("[IdAtendente] IS NOT NULL");

                    b.HasIndex("IdMedico")
                        .IsUnique()
                        .HasFilter("[IdMedico] IS NOT NULL");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("ConsultorioMedico.Domain.Entity.Agendamento", b =>
                {
                    b.HasOne("ConsultorioMedico.Domain.Entity.Medico", "Medico")
                        .WithMany("Agendamentos")
                        .HasForeignKey("IdMedico")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ConsultorioMedico.Domain.Entity.Paciente", "Paciente")
                        .WithMany("Agendamentos")
                        .HasForeignKey("IdPaciente")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("ConsultorioMedico.Domain.Entity.Atendente", b =>
                {
                    b.HasOne("ConsultorioMedico.Domain.Entity.Endereco", "Endereco")
                        .WithMany("Atendentes")
                        .HasForeignKey("IdEndereco")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("ConsultorioMedico.Domain.Entity.Consulta", b =>
                {
                    b.HasOne("ConsultorioMedico.Domain.Entity.Agendamento", "Agendamento")
                        .WithOne("Consulta")
                        .HasForeignKey("ConsultorioMedico.Domain.Entity.Consulta", "IdAgendamento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConsultorioMedico.Domain.Entity.Medico", b =>
                {
                    b.HasOne("ConsultorioMedico.Domain.Entity.Endereco", "Endereco")
                        .WithMany("Medicos")
                        .HasForeignKey("IdEndereco")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("ConsultorioMedico.Domain.Entity.Paciente", b =>
                {
                    b.HasOne("ConsultorioMedico.Domain.Entity.Endereco", "Endereco")
                        .WithMany("Pacientes")
                        .HasForeignKey("IdEndereco")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("ConsultorioMedico.Domain.Entity.Usuario", b =>
                {
                    b.HasOne("ConsultorioMedico.Domain.Entity.Atendente", "Atendente")
                        .WithOne("Usuario")
                        .HasForeignKey("ConsultorioMedico.Domain.Entity.Usuario", "IdAtendente")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ConsultorioMedico.Domain.Entity.Medico", "Medico")
                        .WithOne("Usuario")
                        .HasForeignKey("ConsultorioMedico.Domain.Entity.Usuario", "IdMedico")
                        .OnDelete(DeleteBehavior.NoAction);
                });
#pragma warning restore 612, 618
        }
    }
}
