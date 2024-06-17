using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class PeliculaAlquilableConfig : IEntityTypeConfiguration<PeliculaAlquilable>
	{
		public void Configure(EntityTypeBuilder<PeliculaAlquilable> builder)
		{
			builder.ToTable("PeliculasAlquilables");

			var pelicula = new PeliculaAlquilable()
			{
				Id = 1,
				Nombre = "Spider-Man",
				PeliculaId = 1,
				Precio = 5.99m
			};

			builder.HasData(pelicula);
		}
	}
}
