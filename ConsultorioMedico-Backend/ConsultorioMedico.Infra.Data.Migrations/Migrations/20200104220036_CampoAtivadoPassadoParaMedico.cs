using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsultorioMedico.Infra.Data.Migrations.Migrations
{
    public partial class CampoAtivadoPassadoParaMedico : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativado",
                table: "Usuario");

            migrationBuilder.AddColumn<bool>(
                name: "Ativado",
                table: "Medico",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativado",
                table: "Medico");

            migrationBuilder.AddColumn<bool>(
                name: "Ativado",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
