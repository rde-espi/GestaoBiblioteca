using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoBiblioteca.Migrations
{
    public partial class AddImagemCapaLivro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemCapaUrl",
                table: "Livros",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemCapaUrl",
                table: "Livros");
        }
    }
}
