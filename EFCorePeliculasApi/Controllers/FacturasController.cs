using EFCorePeliculasApi.Entidades;
using EFCorePeliculasApi.Entidades.Funciones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculasApi.Controllers
{
	[ApiController]
	[Route("api/facturas")]
	public class FacturasController : ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly ILogger<FacturasController> logger;

		public FacturasController(ApplicationDbContext context, ILogger<FacturasController> logger)
		{
			this.context = context;
			this.logger = logger;
		}

		[HttpPost]
		public async Task<ActionResult> Post()
		{
			/*
			 para poder realizar y iniciar una transaccion para que 
			no quede los registros huerfanos usamos beginTransaction
			 */
			using var transaccion = await context.Database.BeginTransactionAsync();

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

		/*
		 fn scalares
		 */
		[HttpGet("Fn_Escalares")]
		public async Task<ActionResult> GetFuncionesEscalares()
		{
			//hacemos una proyeccion
			var facturas = await context.Facturas.Select(f => new
			{
				Id = f.Id,
				Total = context.FacturaDetalleSuma(f.Id),//fn definida en el dbcontext
				Promedio = Escalares.FacturaDetallePromedio(f.Id)//fn definida en una clase externa
			})
				.OrderByDescending(f => context.FacturaDetalleSuma(f.Id))
				.ToListAsync();

			return Ok(facturas);
		}

		/*
		 columnas calculadas
		 */
		[HttpGet("{id:int}/detalle")]
		public async Task<ActionResult<IEnumerable<FacturaDetalle>>>GetDetalle(int id)
		{
			return await context.FacturaDetalles.Where(f=>f.FacturaId==id)
				.OrderByDescending(f=>f.Total)
				.ToListAsync();
		}

		/*
		 control de conflitos de concurrencia por fila, con su control de version
		 */
		[HttpPost("concurrencia_Fila")]
		public async Task<ActionResult> ConcurrenciaFila()
		{
			var facturaId = 2;

			var factura= await context.Facturas.AsTracking().FirstOrDefaultAsync(f=>f.Id==facturaId);
			factura.FechaCreacion=DateTime.Now;

			await context.Database.ExecuteSqlInterpolatedAsync($"UPDATE Facturas SET FechaCreacion=GETDATE() WHERE Id={facturaId}");

			await context.SaveChangesAsync();
			return Ok();

		}

		/*
		 manejando error de conflitos de concurrencia
		 */
		[HttpPost("concurrencia_Fila_Manejando_Error")]
		public async Task<ActionResult> ConcurrenciaFilaManejandoError()
		{
			var facturaId = 2;

			/*
			 para manejar el error lo ponemos en un try
			 */

			try
			{
				var factura = await context.Facturas.AsTracking().FirstOrDefaultAsync(f => f.Id == facturaId);
				factura.FechaCreacion = DateTime.Now.AddDays(-10);

				await context.Database.ExecuteSqlInterpolatedAsync($"UPDATE Facturas SET FechaCreacion=GETDATE() WHERE Id={facturaId}");

				await context.SaveChangesAsync();
				return Ok();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				/*
				 para obtener el entry del registro 
					ex.Entries.Single();

				para obtener la lista de las entry
					ex.Entries;
				 */
				var entry = ex.Entries.Single();

				//cuando el registo ya esta en memoria usar .AsNoTracking()
				var facturaActual=await context.Facturas.AsNoTracking().FirstOrDefaultAsync(f=>f.Id==facturaId);

				//interar todas las entry
				foreach (var propiedad in entry.Metadata.GetProperties())
				{
					var valorIntentado = entry.Property(propiedad.Name).CurrentValue;
					var valorDBActual=context.Entry(facturaActual).Property(propiedad.Name).CurrentValue;
					var valorAnterior=entry.Property(propiedad.Name).OriginalValue;

					if (valorDBActual.ToString() == valorIntentado.ToString())
					{
						//no fue modificado la propeidad
						continue;
					}

					logger.LogInformation($"--Propiedad {propiedad.Name}");
					logger.LogInformation($"Valor intentado: {valorIntentado}");
					logger.LogInformation($"Valor DB Actual: {valorDBActual}");
					logger.LogInformation($"Valor anterior: {valorAnterior}");

					//hacer algo
				}

				return BadRequest("Registro no se pudo modificar, ya que esta siendo utilizado por otra persona");
			}

			

		}

		/*
		 manejo de conflicto de concurrencia con el modelo desconectado
		 */
		[HttpGet("ObtenerFactura")]
		public async Task<ActionResult<Factura>>ObtenerFactura(int id)
		{
			var factura= await context.Facturas.FirstOrDefaultAsync(f=>f.Id==id);

			if (factura is null)
				return NotFound();

			return factura;

		}

		[HttpPut("ActualizarFactura")]
		public async Task<ActionResult> ActualizarFactura(Factura factura)
		{
			context.Update(factura);
			await context.SaveChangesAsync();
			return Ok();
		}

	}
}
