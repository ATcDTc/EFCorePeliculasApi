﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class campoMonedaSimbolo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Moneda",
                table: "SalasDeCine",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Moneda",
                table: "SalasDeCine");
        }
    }
}
