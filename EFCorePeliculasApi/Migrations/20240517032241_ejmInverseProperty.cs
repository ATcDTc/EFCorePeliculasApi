using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCorePeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class ejmInverseProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mensajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenido = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmisorId = table.Column<int>(type: "int", nullable: false),
                    ReceptorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensajes_Personas_EmisorId",
                        column: x => x.EmisorId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mensajes_Personas_ReceptorId",
                        column: x => x.ReceptorId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 5, 23, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 16, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 5, 21, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 16, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Jordan" },
                    { 2, "Ester" }
                });

            migrationBuilder.InsertData(
                table: "Mensajes",
                columns: new[] { "Id", "Contenido", "EmisorId", "ReceptorId" },
                values: new object[,]
                {
                    { 1, "Hola, Ester!", 1, 2 },
                    { 2, "Hola, Jordan, como estas tu?", 2, 1 },
                    { 3, "Pura vida!, Y tu?", 1, 2 },
                    { 4, "Gracias a Dios, muy bien!...", 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_EmisorId",
                table: "Mensajes",
                column: "EmisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_ReceptorId",
                table: "Mensajes",
                column: "ReceptorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mensajes");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 5, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 5, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 12, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
