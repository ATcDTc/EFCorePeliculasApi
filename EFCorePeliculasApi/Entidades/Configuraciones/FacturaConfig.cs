using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class FacturaConfig : IEntityTypeConfiguration<Factura>
	{
		public void Configure(EntityTypeBuilder<Factura> builder)
		{
			/*
			 configuracion de begintransaction

			.hasMany, muchas facturas tienen muchos detalles
			.withone, una factura detalle tiene una factura
			builder.HasMany(typeof(FacturaDetalle)).WithOne();
			 */
			builder.HasMany(typeof(FacturaDetalle)).WithOne();
		}
	}
}
