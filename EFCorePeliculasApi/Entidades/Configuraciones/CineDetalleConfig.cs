using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class CineDetalleConfig : IEntityTypeConfiguration<CineDetalle>
	{
		public void Configure(EntityTypeBuilder<CineDetalle> builder)
		{
			/*
			 mapeara con la tabla cine
			 */
			builder.ToTable("Cines");
		}
	}
}
