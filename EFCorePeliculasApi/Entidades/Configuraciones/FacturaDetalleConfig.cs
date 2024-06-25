using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class FacturaDetalleConfig : IEntityTypeConfiguration<FacturaDetalle>
	{
		public void Configure(EntityTypeBuilder<FacturaDetalle> builder)
		{
			/*
			 configurando columna calculada sin guardar el valor
			 */
			builder.Property(f => f.Total)
				//aqui hacemos calculo de una formual o expresion
				.HasComputedColumnSql("Precio*Cantidad"
					/*
					 si queremos guardar el valor en la columna
					ponemos 
						stored:true
					si el calculo entre las comillas es muy lento
					 */
					);
		}
	}
}
