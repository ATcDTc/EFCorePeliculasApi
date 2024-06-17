using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class MerchandisingConfig : IEntityTypeConfiguration<Merchandising>
	{
		public void Configure(EntityTypeBuilder<Merchandising> builder)
		{
			/*
			 que utilice su propia tabla
			 */
			builder.ToTable("Merchandising");

			var merch1=new Merchandising() 
			{
				Id = 2,
				DisponibleEnInventario = true,
				EsRopa=true,
				Nombre="T-Shirt One Piece",
				Peso=1,
				Volumen=1,
				Precio=11
			};

			builder.HasData(merch1);
		}
	}
}
