using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class FacturaConfig : IEntityTypeConfiguration<Factura>
	{
		public void Configure(EntityTypeBuilder<Factura> builder)
		{
			/*
			 configurando la tabla a temporal de forma personalizada
			 */
			builder.ToTable(name: nameof(Factura), op =>
			{
				op.IsTemporal(t =>
				{
					//aca personalizamos las columnas de la tabla temporal
					t.HasPeriodStart("Desde");
					t.HasPeriodEnd("Hasta");
					t.UseHistoryTable(name: "FacturasHistorico");
				});
			});

			//si eixste convenio de datetime2
			builder.Property<DateTime>("Desde").HasColumnType("datetime2");
			builder.Property<DateTime>("Hasta").HasColumnType("datetime2");

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
