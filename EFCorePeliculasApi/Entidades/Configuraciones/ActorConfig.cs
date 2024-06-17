using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class ActorConfig : IEntityTypeConfiguration<Actor>
	{
		public void Configure(EntityTypeBuilder<Actor> builder)
		{
			builder.Property(a => a.Nombre)
				.IsRequired()
				.HasMaxLength(150);

			/*
			 para que la propiedad use el campo _nombre
			con .HasFiel, ponemos el campo a usar
			 */
			builder.Property(n => n.Nombre).HasField("_nombre");

			/*
			 para crear una propiedad en la clase, que nos servira
			para realizar calculos, pero sin la necesidad de que los
			registros o calculos se guarden en la bd
			 */
			builder.Ignore(a => a.Edad);
			/*
			 tambien se puede ignorar clases completas
				
				builder.Ignore(d=>d.nombreDeLaClase);
			 */

			/*
			 configuracion de entidades de propiedad
			 */
			builder.OwnsOne(a => a.DireccionHogar, dir =>
			{
				dir.Property(d => d.Calle).HasColumnName("Calle");
				dir.Property(d => d.Provincia).HasColumnName("Provincia");
				dir.Property(d => d.Pais).HasColumnName("Pais");
			});

		}
	}
}
