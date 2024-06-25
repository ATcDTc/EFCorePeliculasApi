using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCorePeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class datosFacturaEjm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalasDeCine_Cines_CineId",
                table: "SalasDeCine");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 24, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 22, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Facturas",
                columns: new[] { "Id", "FechaCreacion" },
                values: new object[,]
                {
                    { 2, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2024, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "FacturaDetalles",
                columns: new[] { "Id", "FacturaId", "Precio", "Producto" },
                values: new object[,]
                {
                    { 3, 2, 350.99m, "Nada" },
                    { 4, 2, 10m, "Nada" },
                    { 5, 2, 45.50m, "Nada" },
                    { 6, 3, 17.99m, "" },
                    { 7, 3, 14m, "Nada" },
                    { 8, 3, 45m, "" },
                    { 9, 3, 100m, "" },
                    { 10, 4, 371m, "" },
                    { 11, 4, 114.99m, "" },
                    { 12, 4, 425m, "" },
                    { 13, 4, 1000m, "" },
                    { 14, 4, 5m, "" },
                    { 15, 4, 2.99m, "" },
                    { 16, 5, 50m, "" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SalasDeCine_Cines_CineId",
                table: "SalasDeCine",
                column: "CineId",
                principalTable: "Cines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalasDeCine_Cines_CineId",
                table: "SalasDeCine");

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "FacturaDetalles",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Facturas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Facturas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Facturas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Facturas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.AddForeignKey(
                name: "FK_SalasDeCine_Cines_CineId",
                table: "SalasDeCine",
                column: "CineId",
                principalTable: "Cines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
