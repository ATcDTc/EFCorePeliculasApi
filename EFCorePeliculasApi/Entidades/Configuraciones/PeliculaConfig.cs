using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class PeliculaConfig : IEntityTypeConfiguration<Pelicula>
	{
		public void Configure(EntityTypeBuilder<Pelicula> builder)
		{
			builder.Property(p => p.Titulo)
				.HasMaxLength(250)
				.IsRequired();
			/*
			builder.Property(p => p.FechaEstreno)
				.HasColumnType("date");
			*/
			builder.Property(p => p.PosterURL)
				.HasMaxLength(500)
				/*
				esto se refiere a los caracteres que vamos a aceptar en este
				campo, esto se hace para ahorrar espacios, ya que los caracteres
				especiales lo consumen, pero si lo ocuparamos, le ponemos true
					.IsUnicode(false);
				se quito porque se configurara en api fluente de forma automatica
				*/
				;

			/*
			 configuracion de relacion de muchos a muchos, sin clase intermedio o 
			SkipNavigation, 
			
			builder.HasMany(p => p.Generos)//decimos que una pelicula tiene muchos generos
				.WithMany(g=>g.Peliculas)//genero tiene muchas peliculas
				 
					para usar una tabla relacional para esta relacion de muchos a muchos
				 
				 .UsingEntity(t=>t.ToTable("GenerosPeliculas")
				
						 aca podemos pasar o insertar seeding por medio de .hasdata
						y usar un objeto anonimo
				
					.HasData(
						new {PeliculasId=1,GenerosIdentificador=7})
					);
			*/

		}
	}
}
