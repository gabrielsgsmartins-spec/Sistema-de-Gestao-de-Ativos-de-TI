using System;
using Microsoft.EntityFrameworkCore.Migrations;
using SistemadeGestãodeAtivosdeTI.Data;
#nullable disable

namespace SistemadeGestãodeAtivosdeTI.Migrations
{
    /// <inheritdoc />
    public partial class addModelmanutencao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataEntradaManutencao",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "DataSaidaManutencao",
                table: "Equipamentos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataEntradaManutencao",
                table: "Equipamentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataSaidaManutencao",
                table: "Equipamentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
