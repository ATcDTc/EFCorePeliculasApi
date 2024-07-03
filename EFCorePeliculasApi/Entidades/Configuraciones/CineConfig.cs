using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EFCorePeliculasApi.Entidades.Configuraciones
{
	public class CineConfig : IEntityTypeConfiguration<Cine>
	{

		//public void Configure(EntityTypeBuilder<Cine> builder)
		//{

		//	builder.Property(c => c.Nombre)
		//		.HasMaxLength(150)
		//		.IsRequired();


		//		// configuracion con el api fluente, para relaciones
		//		//de uno a uno, para esto utilizamos .HasOne(), y dentro
		//		//ponemos la propiedad de navegacion de donde hacia donde
		//		//por medio de .WithOne(), 
		//		//	si fuera el caso tambien se le
		//		//	puede poner una propiedad de navegacion dentro de los 
		//		//	parentesis,
		//		//	ejm:
		//		//		si CineOferta, tuviera una propiedad hacia Cine,
		//		//		se le pone su propiedad dentro de los parentesis

		//	builder.HasOne(c => c.CineOferta)

		//			//nos permite realizar la navegacion

		//		.WithOne()

		//			// configuracion de una llave foreign key, que no
		//			//siga la convencion, se configura de esta manera

		//		.HasForeignKey<CineOferta>(co=>co.CineId);


		//		// relacion de uno a muchos por medio del api fluente, por medio
		//		//de .HasMany() y despues WithOne()

		//	builder.HasMany(c => c.SalasDeCine)

		//			// aca referimos que una sala tiene un cine con y por
		//			//medio de .WithOne()

		//		.WithOne(s=>s.Cine)

		//			//podemos configurar la foreign key, aca

		//		.HasForeignKey(s=>s.CineId)

		//			// configuracion del OnDelete()
		//			//elegimos el tipo de valor segun sea necesario

		//		.OnDelete(DeleteBehavior.Cascade);


		//		// configurando la relacion de uno a uno para
		//		//la tecnica de division de tablas

		//	builder.HasOne(c=>c.CineDetalle).WithOne(cd=>cd.Cine)
		//		//configurando la ForeignKey con el id de CineDetalle
		//		.HasForeignKey<CineDetalle>(cd=>cd.Id);


		//		// configuracion para entidades de propiedad, configuracion de su
		//		//nombre de columna
		//		//.OwnsOne, nos permite decirle que es dueño de una entidad

		//	builder.OwnsOne(c => c.Direccion, dir =>
		//	{

		//			// configuracion de cada una de las propiedades
		//			//y sus nombres de columna

		//		dir.Property(d => d.Calle).HasColumnName("Calle");
		//		dir.Property(d => d.Provincia).HasColumnName("Provincia");
		//		dir.Property(d => d.Pais).HasColumnName("Pais");
		//	});

		//}

		public void Configure(EntityTypeBuilder<Cine> builder)
		{
			/*
			 cambiando la estrategia de deteccion de cambios
			por la personalizada

				builder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications);
			 */

			builder.HasChangeTrackingStrategy(ChangeTrackingStrategy.ChangedNotifications);

			builder.Property(c => c.Nombre)
				.HasMaxLength(150)
				.IsRequired();

			/*
			 configuracion con el api fluente, para relaciones
			de uno a uno, para esto utilizamos .HasOne(), y dentro
			ponemos la propiedad de navegacion de donde hacia donde
			por medio de .WithOne(), 
				si fuera el caso tambien se le
				puede poner una propiedad de navegacion dentro de los 
				parentesis,
				ejm:
					si CineOferta, tuviera una propiedad hacia Cine,
					se le pone su propiedad dentro de los parentesis
			*/
			builder.HasOne(c => c.CineOferta)
				/*
				 nos permite realizar la navegacion
				 */
				.WithOne()
				/*
				 configuracion de una llave foreign key, que no
				siga la convencion, se configura de esta manera
				 */
				.HasForeignKey<CineOferta>(co => co.CineId);

			/*
			 relacion de uno a muchos por medio del api fluente, por medio
			de .HasMany() y despues WithOne()
			 */
			builder.HasMany(c => c.SalasDeCine)
				/*
				 aca referimos que una sala tiene un cine con y por
				medio de .WithOne()
				 */
				.WithOne(s => s.Cine)
				/*
				 podemos configurar la foreign key, aca
				 */
				.HasForeignKey(s => s.CineId)
				/*
				 configuracion del OnDelete()
				elegimos el tipo de valor segun sea necesario
				 */
				.OnDelete(DeleteBehavior.Cascade);

			/*
			 configurando la relacion de uno a uno para
			la tecnica de division de tablas
			 */
			builder.HasOne(c => c.CineDetalle).WithOne(cd => cd.Cine)
				//configurando la ForeignKey con el id de CineDetalle
				.HasForeignKey<CineDetalle>(cd => cd.Id);

			/*
			 configuracion para entidades de propiedad, configuracion de su
			nombre de columna
			.OwnsOne, nos permite decirle que es dueño de una entidad
			 */
			builder.OwnsOne(c => c.Direccion, dir =>
			{
				/*
				 configuracion de cada una de las propiedades
				y sus nombres de columna
				 */
				dir.Property(d => d.Calle).HasColumnName("Calle");
				dir.Property(d => d.Provincia).HasColumnName("Provincia");
				dir.Property(d => d.Pais).HasColumnName("Pais");
			});

		}
	}
}
