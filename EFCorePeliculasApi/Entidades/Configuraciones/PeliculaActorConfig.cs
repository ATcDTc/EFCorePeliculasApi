using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class PeliculaActorConfig : IEntityTypeConfiguration<PeliculaActor>
	{
		public void Configure(EntityTypeBuilder<PeliculaActor> builder)
		{
			/*
			 configuracion de relaciones de muchos a muchos de forma manual
			
			 configurando las llave primary key compuesta, de forma manual
			 */
			builder.HasKey(
				pa => new
				{
					pa.PeliculaId,
					pa.ActorId
				}
				);

			/*
			 configuracion de muchos a muchos, por medio del api fluente
			en cada una de las foreign key, por medio de cada una de las relaciones
			 */
			builder
				/*
				 se usa .HasOne(), ya que una pelicula tiene un actor
				 */
				.HasOne(pa => pa.Actor)
				/*
				 se usa .WithMany, porque un actor tiene muchas peliculas 
				 */
				.WithMany(a=>a.PeliculasActores)
				.HasForeignKey(pa=>pa.ActorId);

			builder
				/*
				 configuracion de uno a muchos de peliculas a peliculasActores
				ya que una peliculaActores tiene una pelicula
				 */
				.HasOne(pa => pa.Pelicula)
				/*
				 y una pelicula tiene muchos peliculasActores
				 */
				.WithMany(p=>p.PeliculasActores)
				.HasForeignKey(pa=>pa.PeliculaId);

			builder.Property(pa => pa.Personaje)
				.HasMaxLength(150);

		}
	}
}
