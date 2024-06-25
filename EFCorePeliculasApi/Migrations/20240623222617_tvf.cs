using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class tvf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER FUNCTION PeliculaConConteosFn
	(
		@peliculaId INT
	)
RETURNS TABLE
AS
RETURN
(
	SELECT Id, Titulo
			,(
				SELECT COUNT(gp.PeliculasId)
					FROM GeneroPelicula gp
					WHERE gp.PeliculasId=p.Id
			) AS CantidadGeneros
			,(
				SELECT COUNT(DISTINCT(sc.CineId))
					FROM PeliculaSalaDeCine psc
					INNER JOIN SalasDeCine sc
						ON sc.Id=psc.SalasDeCineId
					WHERE psc.PeliculasId=p.Id
			) AS CantidadCines
			,(
				SELECT COUNT(pa.ActorId)
					FROM PeliculasActores pa
					WHERE pa.PeliculaId=p.Id
			) AS CantidadActores
		FROM Peliculas p
		WHERE p.Id=@peliculaId
)
GO");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION DBO.PeliculaConConteosFn");

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
        }
    }
}
