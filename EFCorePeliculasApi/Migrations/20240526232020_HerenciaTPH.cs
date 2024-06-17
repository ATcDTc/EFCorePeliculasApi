using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCorePeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class HerenciaTPH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    FechaTransaccion = table.Column<DateTime>(type: "date", nullable: false),
                    TipoPago = table.Column<int>(type: "int", nullable: false),
                    NumeroWallet = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Ultimos4Digitos = table.Column<string>(type: "CHAR(4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 2, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 26, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 5, 31, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 26, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Pagos",
                columns: new[] { "Id", "FechaTransaccion", "Monto", "TipoPago", "Ultimos4Digitos" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 500m, 2, "1234" },
                    { 2, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 120m, 2, "4321" }
                });

            migrationBuilder.InsertData(
                table: "Pagos",
                columns: new[] { "Id", "CorreoElectronico", "FechaTransaccion", "Monto", "TipoPago" },
                values: new object[,]
                {
                    { 3, "echo@echo.com", new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 157m, 1 },
                    { 4, "echo2@echo2.com", new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 9.99m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Pagos",
                columns: new[] { "Id", "FechaTransaccion", "Monto", "NumeroWallet", "TipoPago" },
                values: new object[,]
                {
                    { 5, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 500m, "vaijwo9849hga", 3 },
                    { 6, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 400m, "9a8s749v7a90847a098", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagos");

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
    }
}
