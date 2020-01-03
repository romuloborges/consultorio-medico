using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsultorioMedico.Infra.Data.Migrations.Migrations
{
    public partial class SeedUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Usuario(IdUsuario, Email, Senha, Tipo, IdMedico, IdAtendente) VALUES(NEWID(), 'admin@email.com', '4f5f282e7e716424bcd5b5a10a82d7acabc87a0ae07ee88d9fd8ae69bbfbbfc9', 'Administrador', null, null)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Usuario");
        }
    }
}
