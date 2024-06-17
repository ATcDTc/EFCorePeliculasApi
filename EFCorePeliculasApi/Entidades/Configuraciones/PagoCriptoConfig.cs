using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class PagoCriptoConfig : IEntityTypeConfiguration<PagoCripto>
	{
		public void Configure(EntityTypeBuilder<PagoCripto> builder)
		{
			builder.Property(b=>b.NumeroWallet).HasMaxLength(256);

			//seeding
			var pago = new PagoCripto[]
			{
				new PagoCripto
				{
					Id = 5,
					FechaTransaccion=new DateTime(2024,1,7),
					Monto=500,
					TipoPago=TipoPago.Cripto,
					NumeroWallet="vaijwo9849hga"
				},
				new PagoCripto {
					Id = 6,
					FechaTransaccion=new DateTime(2024,1,8),
					Monto=400,
					TipoPago=TipoPago.Cripto,
					NumeroWallet="9a8s749v7a90847a098"
				}
			};

			builder.HasData(pago);
		}
	}
}
