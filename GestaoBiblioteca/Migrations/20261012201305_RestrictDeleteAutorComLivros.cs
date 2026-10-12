using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoBiblioteca.Migrations
{
    public partial class RestrictDeleteAutorComLivros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivrosAutores_Autores_AutorId",
                table: "LivrosAutores");

            migrationBuilder.AddForeignKey(
                name: "FK_LivrosAutores_Autores_AutorId",
                table: "LivrosAutores",
                column: "AutorId",
                principalTable: "Autores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivrosAutores_Autores_AutorId",
                table: "LivrosAutores");

            migrationBuilder.AddForeignKey(
                name: "FK_LivrosAutores_Autores_AutorId",
                table: "LivrosAutores",
                column: "AutorId",
                principalTable: "Autores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
