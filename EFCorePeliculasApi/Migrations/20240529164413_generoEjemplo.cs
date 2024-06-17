using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class generoEjemplo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ejemplo",
                table: "Generos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 5, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 29, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 29, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Generos",
                keyColumn: "Identificador",
                keyValue: 1,
                column: "Ejemplo",
                value: null);

            migrationBuilder.UpdateData(
                table: "Generos",
                keyColumn: "Identificador",
                keyValue: 2,
                column: "Ejemplo",
                value: null);

            migrationBuilder.UpdateData(
                table: "Generos",
                keyColumn: "Identificador",
                keyValue: 3,
                column: "Ejemplo",
                value: null);

            migrationBuilder.UpdateData(
                table: "Generos",
                keyColumn: "Identificador",
                keyValue: 4,
                column: "Ejemplo",
                value: null);

            migrationBuilder.UpdateData(
                table: "Generos",
                keyColumn: "Identificador",
                keyValue: 5,
                column: "Ejemplo",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ejemplo",
                table: "Generos");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 28, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 28, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
