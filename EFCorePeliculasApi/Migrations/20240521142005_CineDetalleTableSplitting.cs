using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class CineDetalleTableSplitting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoEtica",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Historia",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Misiones",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Valores",
                table: "Cines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoEtica",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "Historia",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "Misiones",
                table: "Cines");

            migrationBuilder.DropColumn(
                name: "Valores",
                table: "Cines");
        }
    }
}
