using EFCorePeliculasApi.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculasApi.Controllers
{
	/*
	 inicializacion basica para un controller
	de herencia - tabla jerarquia
	 */
	[ApiController]
	[Route("api/pagos")]
	public class PagosController:ControllerBase
	{
		private readonly ApplicationDbContext context;

		public PagosController(ApplicationDbContext context)
        {
			this.context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Pago>>> Get()
		{
			return await context.Pagos.ToListAsync();
		}

		[HttpGet("tarjetas")]
		public async Task<ActionResult<IEnumerable<PagoTarjeta>>> GetTarjetas()
		{
			return await context.Pagos
				/*
				 .OfType<>, nos permite traer, un tipo de pago en especifico o tipo
				ya con esto EFC, sabra que columnas y data traer, para mostrar la informacion
				 */
				.OfType<PagoTarjeta>().ToListAsync();
		}

		[HttpGet("paypal")]
		public async Task<ActionResult<IEnumerable<PagoPaypal>>> GetPaypal()
		{
			return await context.Pagos.OfType<PagoPaypal>().ToListAsync();
		}

		[HttpGet("cripto")]
		public async Task<ActionResult<IEnumerable<PagoCripto>>> GetCripto()
		{
			return await context.Pagos.OfType<PagoCripto>().ToListAsync();
		}
    }
}
