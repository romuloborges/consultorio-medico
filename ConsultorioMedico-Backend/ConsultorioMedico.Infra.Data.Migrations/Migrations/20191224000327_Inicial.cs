using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsultorioMedico.Infra.Data.Migrations.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    IdEndereco = table.Column<Guid>(nullable: false),
                    Cep = table.Column<string>(maxLength: 9, nullable: false),
                    Logradouro = table.Column<string>(maxLength: 100, nullable: false),
                    Complemento = table.Column<string>(maxLength: 50, nullable: false),
                    Bairro = table.Column<string>(maxLength: 50, nullable: false),
                    Localidade = table.Column<string>(maxLength: 100, nullable: false),
                    Uf = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.IdEndereco);
                });

            migrationBuilder.CreateTable(
                name: "Atendente",
                columns: table => new
                {
                    IdAtendente = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    Sexo = table.Column<string>(maxLength: 1, nullable: false),
                    Cpf = table.Column<string>(nullable: true),
                    Rg = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Telefone = table.Column<string>(maxLength: 14, nullable: false),
                    IdEndereco = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendente", x => x.IdAtendente);
                    table.ForeignKey(
                        name: "FK_Atendente_Endereco_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "Endereco",
                        principalColumn: "IdEndereco");
                });

            migrationBuilder.CreateTable(
                name: "Medico",
                columns: table => new
                {
                    IdMedico = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Cpf = table.Column<string>(nullable: true),
                    Rg = table.Column<string>(nullable: true),
                    Crm = table.Column<int>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    Sexo = table.Column<string>(maxLength: 1, nullable: false),
                    Telefone = table.Column<string>(maxLength: 14, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    IdEndereco = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.IdMedico);
                    table.ForeignKey(
                        name: "FK_Medico_Endereco_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "Endereco",
                        principalColumn: "IdEndereco");
                });

            migrationBuilder.CreateTable(
                name: "Paciente",
                columns: table => new
                {
                    IdPaciente = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    NomeSocial = table.Column<string>(maxLength: 100, nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    Sexo = table.Column<string>(maxLength: 1, nullable: false),
                    Cpf = table.Column<string>(nullable: true),
                    Rg = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(maxLength: 14, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    IdEndereco = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paciente", x => x.IdPaciente);
                    table.ForeignKey(
                        name: "FK_Paciente_Endereco_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "Endereco",
                        principalColumn: "IdEndereco");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(maxLength: 25, nullable: false),
                    Tipo = table.Column<string>(maxLength: 50, nullable: false),
                    IdMedico = table.Column<Guid>(nullable: true),
                    IdAtendente = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Atendente_IdAtendente",
                        column: x => x.IdAtendente,
                        principalTable: "Atendente",
                        principalColumn: "IdAtendente");
                    table.ForeignKey(
                        name: "FK_Usuario_Medico_IdMedico",
                        column: x => x.IdMedico,
                        principalTable: "Medico",
                        principalColumn: "IdMedico");
                });

            migrationBuilder.CreateTable(
                name: "Agendamento",
                columns: table => new
                {
                    IdAgendamento = table.Column<Guid>(nullable: false),
                    DataHoraAgendamento = table.Column<DateTime>(nullable: false),
                    DataHoraRegistro = table.Column<DateTime>(nullable: false),
                    IdMedico = table.Column<Guid>(nullable: false),
                    IdPaciente = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamento", x => x.IdAgendamento);
                    table.ForeignKey(
                        name: "FK_Agendamento_Medico_IdMedico",
                        column: x => x.IdMedico,
                        principalTable: "Medico",
                        principalColumn: "IdMedico");
                    table.ForeignKey(
                        name: "FK_Agendamento_Paciente_IdPaciente",
                        column: x => x.IdPaciente,
                        principalTable: "Paciente",
                        principalColumn: "IdPaciente");
                });

            migrationBuilder.CreateTable(
                name: "Consulta",
                columns: table => new
                {
                    IdConsulta = table.Column<Guid>(nullable: false),
                    DataHoraTerminoConsulta = table.Column<DateTime>(nullable: false),
                    Observacoes = table.Column<string>(nullable: false),
                    IdAgendamento = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consulta", x => x.IdConsulta);
                    table.ForeignKey(
                        name: "FK_Consulta_Agendamento_IdAgendamento",
                        column: x => x.IdAgendamento,
                        principalTable: "Agendamento",
                        principalColumn: "IdAgendamento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_IdMedico",
                table: "Agendamento",
                column: "IdMedico");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_IdPaciente",
                table: "Agendamento",
                column: "IdPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_Atendente_Cpf",
                table: "Atendente",
                column: "Cpf",
                unique: true,
                filter: "[Cpf] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Atendente_IdEndereco",
                table: "Atendente",
                column: "IdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_Atendente_Rg",
                table: "Atendente",
                column: "Rg",
                unique: true,
                filter: "[Rg] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_IdAgendamento",
                table: "Consulta",
                column: "IdAgendamento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medico_Cpf",
                table: "Medico",
                column: "Cpf",
                unique: true,
                filter: "[Cpf] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Medico_Crm",
                table: "Medico",
                column: "Crm",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medico_IdEndereco",
                table: "Medico",
                column: "IdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_Medico_Rg",
                table: "Medico",
                column: "Rg",
                unique: true,
                filter: "[Rg] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_Cpf",
                table: "Paciente",
                column: "Cpf",
                unique: true,
                filter: "[Cpf] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_IdEndereco",
                table: "Paciente",
                column: "IdEndereco");

            migrationBuilder.CreateIndex(
                name: "IX_Paciente_Rg",
                table: "Paciente",
                column: "Rg",
                unique: true,
                filter: "[Rg] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdAtendente",
                table: "Usuario",
                column: "IdAtendente",
                unique: true,
                filter: "[IdAtendente] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IdMedico",
                table: "Usuario",
                column: "IdMedico",
                unique: true,
                filter: "[IdMedico] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consulta");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Agendamento");

            migrationBuilder.DropTable(
                name: "Atendente");

            migrationBuilder.DropTable(
                name: "Medico");

            migrationBuilder.DropTable(
                name: "Paciente");

            migrationBuilder.DropTable(
                name: "Endereco");
        }
    }
}
