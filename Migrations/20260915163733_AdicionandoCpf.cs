using Microsoft.EntityFrameworkCore.Migrations;
using SistemadeGestãodeAtivosdeTI.Data;
#nullable disable

namespace SistemadeGestãodeAtivosdeTI.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCpf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Funcionarios");
        }
    }
}
