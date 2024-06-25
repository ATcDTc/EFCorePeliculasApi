using EFCorePeliculasApi.Migrations;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculasApi.Entidades.Seeding
{
	public static class SeedingFacturas
	{
        public static void Seed(ModelBuilder modelBuilder)
        {
            Factura[] facturas = new Factura[]
            {
                new Factura
                {
                    Id=2,
                    FechaCreacion=new DateTime(2024,1,24)
                },
				new Factura
				{
					Id=3,
					FechaCreacion=new DateTime(2024,1,24)
				},
				new Factura
				{
					Id=4,
					FechaCreacion=new DateTime(2024,1,24)
				},
				new Factura
				{
					Id=5,
					FechaCreacion=new DateTime(2024,1,24)
				}
			};

			FacturaDetalle[] facturaDetalles =
			{
				new FacturaDetalle
				{
					Id=3,
					FacturaId=facturas[0].Id,
					Precio=350.99m, Producto="Nada"
				},
				new FacturaDetalle
				{
					Id=4,
					FacturaId=facturas[0].Id,
					Precio=10,Producto="Nada"
				},
				new FacturaDetalle
				{
					Id=5,
					FacturaId=facturas[0].Id,
					Precio=45.50m,Producto="Nada"
				},
				new FacturaDetalle
				{
					Id = 6, FacturaId = facturas[1].Id,Producto="", Precio = 17.99m
				},
				new FacturaDetalle
				{
					Id = 7, FacturaId = facturas[1].Id, Precio = 14,Producto="Nada"
				},
				new FacturaDetalle
				{
					Id = 8, FacturaId = facturas[1].Id, Precio = 45,Producto=""
				},
				new FacturaDetalle
				{
					Id = 9, FacturaId = facturas[1].Id, Precio = 100,Producto=""
				},
				new FacturaDetalle
				{
					Id = 10, FacturaId = facturas[2].Id, Precio = 371,Producto=""
				},
				new FacturaDetalle
				{
					Id = 11, FacturaId = facturas[2].Id, Precio = 114.99m,Producto=""
				},
				new FacturaDetalle
				{
					Id = 12, FacturaId = facturas[2].Id, Precio = 425,Producto=""
				},
				new FacturaDetalle
				{
					Id = 13, FacturaId = facturas[2].Id, Precio = 1000,Producto=""
				},
				new FacturaDetalle
				{
					Id = 14, FacturaId = facturas[2].Id, Precio = 5,Producto=""
				},
				new FacturaDetalle
				{
					Id = 15, FacturaId = facturas[2].Id, Precio = 2.99m,Producto=""
				},
				new FacturaDetalle
				{
					Id = 16, FacturaId = facturas[3].Id, Precio = 50,Producto=""
				}
			};

			modelBuilder.Entity<Factura>().HasData(facturas);
			modelBuilder.Entity<FacturaDetalle>().HasData(facturaDetalles);
        }
    }
}
