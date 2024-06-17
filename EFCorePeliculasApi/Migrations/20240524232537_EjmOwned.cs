using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class EjmOwned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Calle",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Provincia",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_Calle",
                table: "Actores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_Pais",
                table: "Actores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_Provincia",
                table: "Actores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Calle",
                table: "Actores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Actores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Provincia",
                table: "Actores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 24, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 5, 29, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 24, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calle",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "Provincia",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "BillingAddress_Calle",
                table: "Actores");

            migrationBuilder.DropColumn(
                name: "BillingAddress_Pais",
                table: "Actores");

            migrationBuilder.DropColumn(
                name: "BillingAddress_Provincia",
                table: "Actores");

            migrationBuilder.DropColumn(
                name: "Calle",
                table: "Actores");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Actores");

            migrationBuilder.DropColumn(
                name: "Provincia",
                table: "Actores");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 5, 28, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 21, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 5, 26, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 21, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
