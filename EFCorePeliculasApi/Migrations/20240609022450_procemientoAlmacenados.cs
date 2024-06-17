using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class procemientoAlmacenados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
             creando procedimiento almacenados
             */
            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE S_GeneroObtenerPorId
	@id int
AS
SET NOCOUNT ON
BEGIN
	SELECT TOP 1 *
		FROM Generos
		WHERE Identificador=@id
END
GO");

            migrationBuilder.Sql(@"CREATE OR ALTER PROCEDURE I_Generos
@nombre NVARCHAR(150),
@id INT OUTPUT --nos permite que salga la variable
AS
SET NOCOUNT ON
BEGIN
	INSERT INTO Generos(Nombre)
		VALUES (@nombre)

	SELECT @id=SCOPE_IDENTITY()
END
GO");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 13, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 8, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE dbo.I_Generos");
            migrationBuilder.Sql("DROP PROCEDURE dbo.S_GeneroObtenerPorId");

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 6, 9, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 6, 4, 0, 0, 0, 0, DateTimeKind.Local) });
        }
    }
}
