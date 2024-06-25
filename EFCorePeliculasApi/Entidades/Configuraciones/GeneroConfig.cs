using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class GeneroConfig : IEntityTypeConfiguration<Genero>
	{
		public void Configure(EntityTypeBuilder<Genero> builder)
		{
			/*
			 aca ponemos todas las configuraciones del api fuente
			esto se hace para poder tener un orden dentro del entity framework core
			y tambien para poder darle mantenimiento con mas legibilidad

			configurando dicha entidad como una tabla temporal
			 */


			

			//aca estamos diciendo cual campo es la llave primaria
			builder.HasKey(g => g.Identificador);
			/*
			 podemos configurar aca los limites de los campos
			con .Property y .HasMaxLength()
			 */
			builder.Property(g => g.Nombre)
				//podemos configurar mas de una propiedad
				.HasMaxLength(150)
				//para poner que el campo no permita nulos
				.IsRequired()
				/*
				 podemos ponerle el nombre que queramos
					.HasColumnName("nombreGenero");

				para evitar los problemas de concurrencia por campo
				en este campo
					.IsConcurrencyToken()
				 */
				.IsConcurrencyToken();

			/*
			  para darle otro nombre a la tabla distinta a la clase
				  .ToTable(name: "TablaGeneros", schema: "peliculas")
				builder.ToTable(name: "TablaGeneros", schema: "peliculas");
			

			 aplicando un filtro a nivel de modelo, para tenerlo como valor por 
			defecto en nuestras consultas, en todo el proyecto con .HasQueryFilter
				
				builder.HasQueryFilter(g => !g.EstaBorrado);
			 */
			builder.HasQueryFilter(g => !g.EstaBorrado);

			/*
			 para la configuracion de un indice de un campo en especifico, ya que 
			facilitara la velocidad de busqueda y no permitira que los registros 
			tengan duplicados
				builder.HasIndex(g => g.Nombre).IsUnique();

			para permitir el ingreso de nombres de forma parcial, siempre y cuando no
			este borrado logico o borrado suave, entonces se permita el registro, aunque
			este quizas este repetido pero con diferente id, solo aca en el api fuente
			se puede configurar, utilizando .HasFilter("condicionAplicar")
				
				builder.HasIndex(g => g.Nombre).IsUnique().HasFilter("EstaBorrado='false'");

			 */
			builder.HasIndex(g => g.Nombre).IsUnique().HasFilter("EstaBorrado='false'");

			/*
			 configuracion de una propiedad sombra
			 */
			builder.Property<DateTime>("FechaCreacion")
				/*
				 declarar valor por defecto en lenguaje sql
				 */
				.HasDefaultValueSql("GETDATE()")
				/*
				 tipo de dato
				 */
				.HasColumnType("DATETIME2");

		}
	}
}
