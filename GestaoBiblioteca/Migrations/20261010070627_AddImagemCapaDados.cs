using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoBiblioteca.Migrations
{
    public partial class AddImagemCapaDados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemCapaUrl",
                table: "Livros");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImagemCapaDados",
                table: "Livros",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagemCapaTipo",
                table: "Livros",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemCapaDados",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "ImagemCapaTipo",
                table: "Livros");

            migrationBuilder.AddColumn<string>(
                name: "ImagemCapaUrl",
                table: "Livros",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
