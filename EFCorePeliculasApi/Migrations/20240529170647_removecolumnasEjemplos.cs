using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class removecolumnasEjemplos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ejemplo",
                table: "Generos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ejemplo",
                table: "Generos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

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
    }
}
