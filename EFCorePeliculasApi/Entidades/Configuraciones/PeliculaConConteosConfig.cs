using EFCorePeliculasApi.Entidades.SinLlaves;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class PeliculaConConteosConfig : IEntityTypeConfiguration<PeliculaConConteos>
	{
		public void Configure(EntityTypeBuilder<PeliculaConConteos> builder)
		{
			/*
			 usando una clase sin llaves, con un query que usa una vista, creada en la bd
			
				builder
					.HasNoKey()
			
					//aca pondremos el nombre de nuestra vista, creada en la bd
					.ToView("PeliculasConConteos");
	
			 Utilizacion de ToSqlQuery, para mapear un querie
			 */
			builder.ToSqlQuery(@"SELECT Id, Titulo
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
		FROM Peliculas p").ToView(null);


		}
	}
}
