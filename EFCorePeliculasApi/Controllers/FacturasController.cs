using EFCorePeliculasApi.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace EFCorePeliculasApi.Controllers
{
	[ApiController]
	[Route("api/facturas")]
	public class FacturasController:ControllerBase
	{
		private readonly ApplicationDbContext context;

		public FacturasController(ApplicationDbContext context)
        {
			this.context = context;
		}

		[HttpPost]
		public async Task<ActionResult> Post()
		{
			/*
			 para poder realizar y iniciar una transaccion para que 
			no quede los registros huerfanos usamos beginTransaction
			 */
			using var transaccion=await context.Database.BeginTransactionAsync();

			try
			{
				var factura = new Factura()
				{
					FechaCreacion = DateTime.Now
				};

				/*
				 necesitamos crear la factura porque necesitamos el id
				para el detalle de esta, por es lo guardamos antes
				 */

				context.Add(factura);
				await context.SaveChangesAsync();

				/*
				 generando un error para poder realizar un test
					throw new ApplicationException("Eso es una prueba");
				 */
				throw new ApplicationException("Eso es una prueba");

				var facturaDetalle = new List<FacturaDetalle>()
				{
					new FacturaDetalle()
					{
						FacturaId = factura.Id,
						Producto="Producto A",
						Precio=123
					},
					new FacturaDetalle()
					{
						FacturaId=factura.Id,
						Producto="Producto B",
						Precio=456
					}
				};

				context.AddRange(facturaDetalle);
				await context.SaveChangesAsync();

				/*
				 hasta aqui, la transaccion no se ha realizado en la bd
				entonces debemos decirle a la transaccion que ya todo esta listo 
				para ello usamos commit transaction, para que persista dicho registro
				en la bd
				 */
				await transaccion.CommitAsync();
				return Ok("Registrado");
			}
			catch (Exception)
			{
				return BadRequest("Hubo un error");
			}

			

		}
    }
}
