using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculasApi.Entidades.Seeding
{
	public static class SeedingPersonaMensaje
	{
        public static void Seed(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Persona>().Property(p => p.Nombre).HasMaxLength(20);
			modelBuilder.Entity<Mensaje>().Property(m=>m.Contenido).HasMaxLength(50);

            var jordan=new Persona() { Id = 1,Nombre="Jordan" };
			var ester = new Persona() { Id = 2, Nombre = "Ester" };

			Mensaje[] mensaje = {
				new Mensaje() { Id = 1, Contenido = "Hola, Ester!", EmisorId = jordan.Id, ReceptorId = ester.Id },
				new Mensaje() { Id = 2, Contenido="Hola, Jordan, como estas tu?",EmisorId=ester.Id, ReceptorId=jordan.Id},
				new Mensaje() { Id=3,Contenido="Pura vida!, Y tu?",EmisorId=jordan.Id,ReceptorId=ester.Id},
				new Mensaje() { Id=4,Contenido="Gracias a Dios, muy bien!...",EmisorId=ester.Id,ReceptorId=jordan.Id}
			}; ;

			/*
			var mensaje1 = new Mensaje() { Id = 1, Contenido = "Hola, Ester!", EmisorId = jordan.Id, ReceptorId = ester.Id };
			var mensaje2 = new Mensaje() { Id = 2, Contenido="Hola, Jordan, como estas tu?",EmisorId=ester.Id, ReceptorId=jordan.Id};
			var mensaje3 = new Mensaje() { Id=3,Contenido="Pura vida!, Y tu?",EmisorId=jordan.Id,ReceptorId=ester.Id};
			var mensaje4=new Mensaje() { Id=4,Contenido="Gracias a Dios, muy bien!...",EmisorId=ester.Id,ReceptorId=jordan.Id};
			*/

			modelBuilder.Entity<Persona>().HasData(jordan, ester);
			modelBuilder.Entity<Mensaje>().HasData(mensaje);
		}
    }
}
