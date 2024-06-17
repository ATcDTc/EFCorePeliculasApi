using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class PagoTarjetaConfig : IEntityTypeConfiguration<PagoTarjeta>
	{
		public void Configure(EntityTypeBuilder<PagoTarjeta> builder)
		{
			/*
			 configuracion de un tipo de dato de sqlserver
			 */
			builder.Property(p => p.Ultimos4Digitos).HasColumnType("CHAR(4)").IsRequired();

			//data de prueba
			var pago = new PagoTarjeta[]
			{
				new PagoTarjeta
				{
					Id = 1,
					FechaTransaccion=new DateTime(2024,1,6),
					Monto=500,
					TipoPago=TipoPago.Tarjeta,
					Ultimos4Digitos="1234"
				},
				new PagoTarjeta
				{
					Id=2,
					FechaTransaccion=new DateTime(2024,1,6),
					Monto=120,
					TipoPago=TipoPago.Tarjeta,
					Ultimos4Digitos="4321"
				}
			};

			builder.HasData(pago);
		}
	}
}
