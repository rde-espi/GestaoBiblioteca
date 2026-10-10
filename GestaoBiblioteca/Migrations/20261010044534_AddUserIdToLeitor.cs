using Microsoft.EntityFrameworkCore.Migrations;

namespace GestaoBiblioteca.Migrations
{
    public partial class AddUserIdToLeitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Leitores",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leitores_UserId",
                table: "Leitores",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leitores_AspNetUsers_UserId",
                table: "Leitores",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leitores_AspNetUsers_UserId",
                table: "Leitores");

            migrationBuilder.DropIndex(
                name: "IX_Leitores_UserId",
                table: "Leitores");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Leitores");
        }
    }
}
