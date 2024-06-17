using EFCorePeliculasApi.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculasApi.Controllers
{
	/*
	 controller de herencia - tabla por tipo
	 */
	[ApiController]
	[Route("api/productos")]
	public class ProductosController:ControllerBase
	{
		private readonly ApplicationDbContext context;

		public ProductosController(ApplicationDbContext context)
        {
			this.context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Producto>>> Get()
		{
			return await context.Productos.ToListAsync();
		}

		[HttpGet("Merchs")]
		public async Task<ActionResult<IEnumerable<Merchandising>>> GetMerchs()
		{
			return await context
				/*
				 con esta tecnica se utiliza .set<entidad>, porque dicha fn,
				realiza un query mas eficiente
				 */
				.Set<Merchandising>().ToListAsync();
		}

		[HttpGet("Alquileres")]
		public async Task<ActionResult<IEnumerable<PeliculaAlquilable>>> GetAlquileres()
		{
			return await context.Set<PeliculaAlquilable>().ToListAsync();
		}
    }
}
