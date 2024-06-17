using EFCorePeliculasApi.Entidades.SinLlaves;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class CineSinUbicacionConfig : IEntityTypeConfiguration<CineSinUbicacion>
	{
		public void Configure(EntityTypeBuilder<CineSinUbicacion> builder)
		{
			/*
			 para configurar la entidad sin llaves primarias
			 */
			builder
				/*
				 .HasNoKey(), nos permite crear entidades sin llaves
				 */
				.HasNoKey()
				.ToSqlQuery(
					/*
					 aca metemos el query que ocupamos con el lenguaje de sql server
					 */
					"SELECT Id,Nombre FROM Cines")
				/*
				 para que no se haga parte de la bd y tampoco del schema
				 */
				.ToView(null);
		}
	}
}
