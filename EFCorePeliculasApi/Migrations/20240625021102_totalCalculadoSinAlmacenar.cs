using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class totalCalculadoSinAlmacenar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "FacturaDetalles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "FacturaDetalles",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                computedColumnSql: "Precio*Cantidad");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 24, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 29, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 24, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 5,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 6,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 7,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 8,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 9,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 10,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 11,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 12,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 13,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 14,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 15,
                column: "Cantidad",
                value: 0);

            migrationBuilder.UpdateData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 16,
                column: "Cantidad",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "FacturaDetalles");

            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "FacturaDetalles");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 23, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 28, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 23, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
