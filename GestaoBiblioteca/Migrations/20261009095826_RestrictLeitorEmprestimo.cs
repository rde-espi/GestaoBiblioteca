using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoBiblioteca.Migrations
{
    public partial class RestrictLeitorEmprestimo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Leitores_LeitorId",
                table: "Emprestimos");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Leitores_LeitorId",
                table: "Emprestimos",
                column: "LeitorId",
                principalTable: "Leitores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Leitores_LeitorId",
                table: "Emprestimos");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Leitores_LeitorId",
                table: "Emprestimos",
                column: "LeitorId",
                principalTable: "Leitores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
