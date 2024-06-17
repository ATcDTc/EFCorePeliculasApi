using EFCorePeliculasApi.Entidades.Conversiones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class SalaDeCineConfig : IEntityTypeConfiguration<SalaDeCine>
	{
		public void Configure(EntityTypeBuilder<SalaDeCine> builder)
		{
			builder.Property(sc => sc.Precio)
				.HasPrecision(precision: 9, scale: 2);
			/*
			 para cambiar un valor por defecto de un campo int, existe dos tipos
				.HasDefaultValue("nValorPorDefecto"), que le pasa al SQL desde la app el valor
				.HasDefaultValueSQL("expresionSQL"), puedo crear una expresion de SQLServer,
					que genere ese valor por defecto, ejm: GETDATE(), etc
			 */
			builder.Property(sc => sc.TipoSalaDeCine)
				.HasDefaultValue(TiposSalaDeCine.DosDimensiones)
				/*
				 para convertir un tipo de dato tipo enum a su valor string
				esto hace que se convierta automaticamente a su valor correspondiente
				sea desde la bd o viceversa
				 */
				.HasConversion<string>()
				.HasMaxLength(20);

			/*
			 Configuracion de la conversion personalizada para el campo de tipo 
			moneda que se creo del tipo enum, pero se guardara el simbolo de la moneda
			 */
			builder.Property(sc => sc.Moneda)
				.HasConversion<MonedaASimboloConverter>()
				.HasMaxLength(2);
		}
	}
}
