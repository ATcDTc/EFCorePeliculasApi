using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class PagoPaypalConfig : IEntityTypeConfiguration<PagoPaypal>
	{
		public void Configure(EntityTypeBuilder<PagoPaypal> builder)
		{
			builder.Property(p => p.CorreoElectronico).HasMaxLength(150).IsRequired();


			/*
			 llenando data
			 */
			PagoPaypal[] pagos = new PagoPaypal[]
			{
				new PagoPaypal 
				{
					Id = 3,
					FechaTransaccion=new DateTime(2024,1,7),
					Monto=157,
					TipoPago=TipoPago.Paypal,
					CorreoElectronico="echo@echo.com"
				},
				new PagoPaypal
				{
					Id=4,
					FechaTransaccion=new DateTime(2024,1,7),
					Monto=9.99m,
					TipoPago=TipoPago.Paypal,
					CorreoElectronico="echo2@echo2.com"
				}
			};

			builder.HasData(pagos);

		}
	}
}
