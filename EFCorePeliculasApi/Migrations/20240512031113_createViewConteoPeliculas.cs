using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCorePeliculasApi.Migrations
{
    /// <inheritdoc />
    public partial class createViewConteoPeliculas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 5, 18, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 5, 16, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Local) });

            /*
             vamos a crear una migracion para crear una vista al motor de la bd
            usando migraciones
             */
            migrationBuilder.Sql(@"
CREATE OR ALTER VIEW dbo.PeliculasConConteos
AS
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
GO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 8, 0, 0, 0, 0, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "CinesOfertas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaFinal", "FechaInicio" },
                values: new object[] { new DateTime(2024, 5, 13, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 5, 8, 0, 0, 0, 0, DateTimeKind.Local) });
            /*
             si se produce un error o lo que sea aplicar el drop
             */
            migrationBuilder.Sql("DROP VIEW dbo.PeliculasConConteos");
        }
    }
}
