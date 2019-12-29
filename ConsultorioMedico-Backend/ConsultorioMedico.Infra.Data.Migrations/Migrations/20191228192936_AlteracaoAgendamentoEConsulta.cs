using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsultorioMedico.Infra.Data.Migrations.Migrations
{
    public partial class AlteracaoAgendamentoEConsulta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacoes",
                table: "Consulta");

            migrationBuilder.AddColumn<string>(
                name: "ReceitaMedica",
                table: "Consulta",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                table: "Agendamento",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceitaMedica",
                table: "Consulta");

            migrationBuilder.DropColumn(
                name: "Observacoes",
                table: "Agendamento");

            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                table: "Consulta",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
