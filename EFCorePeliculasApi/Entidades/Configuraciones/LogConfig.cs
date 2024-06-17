using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class LogConfig : IEntityTypeConfiguration<Log>
	{
		public void Configure(EntityTypeBuilder<Log> builder)
		{
			/*
			 nos permite decirle al EFC, que no genere la GUID
			porque nosotros la vamos a generar
			 */
			builder.Property(l=>l.Id).ValueGeneratedNever();
		}
	}
}
