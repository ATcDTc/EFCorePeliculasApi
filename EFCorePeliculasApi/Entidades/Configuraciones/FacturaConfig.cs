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

			/*
			 poniendo el valor de la secuencia en el campo especifico
			dentro de los () esta la instruccion sql que le dara el nuevo
			valor a dicho campo
			 */
			builder.Property(f => f.NumeroFactura)
				.HasDefaultValueSql("NEXT VALUE FOR factura.NumeroFactura");

			/*
			 configurando el evitar conflito de concurrencia por fila
			 */
			builder.Property(f => f.Version).IsRowVersion();
		}
	}
}
