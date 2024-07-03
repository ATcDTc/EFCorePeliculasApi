using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class modificacionSalasCines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Moneda",
                table: "SalasDeCine",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 7, 8, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 1,
                column: "Moneda",
                value: null);

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 2,
                column: "Moneda",
                value: null);

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 3,
                column: "Moneda",
                value: null);

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 4,
                column: "Moneda",
                value: null);

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 5,
                column: "Moneda",
                value: null);

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 6,
                column: "Moneda",
                value: null);

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 7,
                column: "Moneda",
                value: null);

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 8,
                column: "Moneda",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Moneda",
                table: "SalasDeCine",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 1,
                column: "Moneda",
                value: "?");

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 2,
                column: "Moneda",
                value: "?");

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 3,
                column: "Moneda",
                value: "?");

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 4,
                column: "Moneda",
                value: "?");

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 5,
                column: "Moneda",
                value: "?");

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 6,
                column: "Moneda",
                value: "?");

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 7,
                column: "Moneda",
                value: "?");

            migrationBuilder.UpdateData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 8,
                column: "Moneda",
                value: "?");
        }
    }
}
