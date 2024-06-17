using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class PagoConfig : IEntityTypeConfiguration<Pago>
	{
		public void Configure(EntityTypeBuilder<Pago> builder)
		{
			/*
			 Herencia - Tabla por jerarquia
				.HasDscriminator, permite al EFC, que tipo de campo va hacer
				para que EFC, utilice el tipo de valor a usar de acuerdo a la
				clase derivada
			 */
			builder.HasDiscriminator(p=>p.TipoPago)
				/*
				 aca ponemos si el tipo de campo que selecciona es tal
				entonces tal clase usar, sino que utilice la otra clase
					.HasValue<PagoPaypal>(TipoPago.Paypal)
				asi diferenciamos entre cual instancia va utilizar EFC
				 */
				.HasValue<PagoPaypal>(TipoPago.Paypal)
				.HasValue<PagoTarjeta>(TipoPago.Tarjeta)
				.HasValue<PagoCripto>(TipoPago.Cripto);
			
			builder.Property(p => p.Monto).HasPrecision(18, 2);
		}
	}
}
