using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class CineOfertaConfig : IEntityTypeConfiguration<CineOferta>
	{
		public void Configure(EntityTypeBuilder<CineOferta> builder)
		{
			/*
			 para relacionar una tabla con otra, para realizar relaciones
			 */
			builder.Property(co => co.PorcentajeDescuento)
				.HasPrecision(precision: 5, scale: 2);
			/*
			builder.Property(co=>co.FechaInicio)
				.HasColumnType("date");
			builder.Property(co => co.FechaFinal)
				.HasColumnType("date");
			*/
		}
	}
}
