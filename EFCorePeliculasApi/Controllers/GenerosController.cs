using AutoMapper;
using EFCorePeliculasApi.DTOs;
using EFCorePeliculasApi.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculasApi.Controllers
{
	[ApiController]
	[Route("api/generos")]
	public class GenerosController:ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		/*
recibira peticiones https en el api
y manejar peticiones con la entidade de Generos
*/
		public GenerosController(ApplicationDbContext context, IMapper mapper)
        {
			this.context = context;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<IEnumerable<Genero>> Get()
		{
			/*
		     quieries simple, que trae todo 
			return await context.Generos.ToListAsync();

			pero si queremos traer los registros en modo de solo lectura
				.AsNoTracking(), al estar globalizado en el dbcontext, 
				ya no se necesita llamarlo

			filtrado por el campo EstaBorrado = false
				return await context.Generos.Where(g=>!g.EstaBorrado).OrderBy(g=>g.Nombre).ToListAsync();
			si quita el .Where, porque se ha configurado por defecto a nivel de la clase de configuracion 
			en el api fuente de la clase
			

			utilizacion de otro tipo de dato para primary key, en metodos tipo log, solo que esta no es 
			la mejor manera de hacerlo:
				context.Logs.Add(new Log { Mensaje = "Ejecucion del metodo GenerosController.Get" });
			 */
			context.Logs.Add(new Log {
				Id=/*
				    para generar un nuevo GUID
				    */
					Guid.NewGuid(),
				Mensaje = "Ejecucion del metodo GenerosController.Get" 
				});

			await context.SaveChangesAsync();

			return await context.Generos.OrderBy(g=>g.Nombre).ToListAsync();

		}

		[HttpGet("OrdenFechaCreacionDescendiente")]
		public async Task<IEnumerable<Genero>> GetFechaCreacion()
		{
			context.Logs.Add(new Log
			{
				Id =/*
				    para generar un nuevo GUID
				    */
					Guid.NewGuid(),
				Mensaje = "Ejecucion del metodo GenerosController.Get por orden fechaCreacion"
			});

			await context.SaveChangesAsync();

			return await context.Generos
				/*
				 ordenando por fecha de creacion de la entidad
				 */
				.OrderByDescending(g => 
						/*
						 pasaremos la propiedad sombra
						que esta en la tabla pero no en la entidad
						por medio de EFC
						 */
						EF.Property<DateTime>(g,"FechaCreacion")
				
					).ToListAsync();

		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Genero>> ObtenerPorId(int id)
		{
			/*
			 encuentra el primero con el id que el usuario envie
				var genero = await context.Generos
					.FirstOrDefaultAsync(g => g.Identificador == id);
			*/
			/*
			 Queries albitrarios
				usamos .FromSqlRaw()
			 
				var genero = await context.Generos
					//pasamos el script del sql server por medio de .FromSqlRaw()
					.FromSqlRaw("SELECT TOP 1 * FROM Generos WHERE Identificador={0}",id).IgnoreQueryFilters().FirstOrDefaultAsync();
						
			 usando string interpolation con .FromSqlInterpolated()
			 */
			var genero=await context.Generos
				//es parecido al .FromSqlRaw(), solo que podemos usar $
				.FromSqlInterpolated($"SELECT TOP 1 * FROM Generos WHERE Identificador={id}")
				.IgnoreQueryFilters()
				.FirstOrDefaultAsync();


			if (genero is null)
				return NotFound();
			
			return genero;
		}

		[HttpGet("GeneroConFechaCreacion/{id:int}")]
		public async Task<ActionResult<Genero>> ObtenerPorIdYFechaCreacion(int id)
		{
			var genero=await context.Generos
				/*
				 para obtener la propiedad sombra, le agregamos
				.AsTracking
				 */
				.AsTracking()
				.FirstOrDefaultAsync(g=>g.Identificador==id);
			if (genero is null)
				return NotFound();
			/*
			 para obtener el valor de una propiedad sombra que esta en la tabla pero no 
			en la clase de la entidad
			 */
			var fechaCreacion=context.Entry(genero).Property<DateTime>("FechaCreacion").CurrentValue;

			return Ok(
				 /*
				  retornamos un objeto anonimo, para poder mostrar la propiedad sombra
				  */
				 new
					{
					 Id=genero.Identificador,
					 Nombre=genero.Nombre,
					 fechaCreacion
					}
				
				);
		}

		/*
		[HttpGet("primer")]
		public async Task<ActionResult<Genero>> Primer()
		{
			para obtener el primer registro de la tabla genero
				return await context.Generos.FirstAsync();

			para buscar por un filtro, pero es propenso a errores
				return await context.Generos.FirstAsync(g=> g.Nombre.StartsWith("Z"));
			si no hubiera datos entonces return null, con FirstOrDefaultAsync()
				
			
			var g= await context.Generos.FirstOrDefaultAsync(g=> g.Nombre.StartsWith("C"));

			if (g is null)
				return NotFound();
			
			return Ok(g);
		}

		[HttpGet("filtrar")]
					 se usa un IEnumerable de Genero, porque devolvera varios elementos
		public async Task<IEnumerable<Genero>> Filtrar()
		{
			return await context.Generos.Where(
					g => g.Nombre.StartsWith("C")
					//se puede usar operadores logicos como || o &&
					|| g.Nombre.StartsWith("A")
				).ToListAsync();
		}
		
		//se puede filtrar por un valor introducido por el usuario
		public async Task<IEnumerable<Genero>> Filtrar(string nombre)
		{
			return await context.Generos
				.Where(g => g.Nombre.Contains(nombre))
				
				ordenar por cualquier criterio
					.OrderBy(g => g.Nombre)
				tambien se puede ordenar descendientemente
					.OrderByDescending(g => g.Nombre)
				
				.OrderByDescending(g => g.Nombre)
				.ToListAsync();
		}

		[HttpGet("paginacion")]
		public async Task<ActionResult<IEnumerable<Genero>>> GetPaginacion(int pagina=1)
		{

			
			 para tomar un top de cantidad de registros de una consulta
				.Take(nNumeroTop)
			
			var generos=await context.Generos
				
				 podemos saltar los primeros registros con
					.Skip()
				con ambos podemos crear paginaciones
				
				.Skip(1)
				.Take(2).ToListAsync();
			
			
			 podemos crear paginaciones con ambos .Skip y .Take
			 
				var cantidadRegistrosPorPagina = 2;
				var generos = await context.Generos
					.Skip((pagina-1)*cantidadRegistrosPorPagina)
					.Take(cantidadRegistrosPorPagina).ToListAsync();

			return generos;
		}
		*/

		[HttpPost]//guardando datos
		public async Task<ActionResult> Post(Genero genero)
		{
			/*
			 podemos ver el estado de la entidad con esta propiedad
			aca es detached, que no le esta dando seguimiento, porque 
				EFC, no sabe nada de esta entidad ya que viene de un 
				post
			
				var status1=context.Entry(genero).State;

			
			 aca estamos agregando una nueva entidada para ser registrada
			ya que cambiamos el estado a agregado,
			
				context.Add(genero);
				status1 = context.Entry(genero).State;
			
			 aca lo registra a la bd, siempre que EFC, le este dando seguimiento
			
				await context.SaveChangesAsync();
			
			estado unchanged, que quiere decir que ya EFC ya lo libero, y no hay cambios
			por realizar

				status1 = context.Entry(genero).State;
			*/
			
			/*
			 como se creo un index en el campo nombre de la tabla,
			entonces tener control del error
			 */
			var existeGeneroPelicula= await context.Generos.AnyAsync(g=>g.Nombre==genero.Nombre);

			if (existeGeneroPelicula)
				return BadRequest($"Ya existe un genero con este nombre: {genero.Nombre}");

			/*
			 la importancia de alterar el status del entity framework core
				
				context.Add(genero);
			
			podemos hacer el cambio de status directamente por medio de Entry
				
				context.Entry(genero).State=EntityState.Added;

			podemos realizar la insercion por medio de sentencias arbitrarias, por medio de la fn
			.ExecuteSqlInterpolatedAsync()

			 */
			await context.Database.ExecuteSqlInterpolatedAsync($"INSERT INTO Generos (Nombre) VALUES ({genero.Nombre})");
			
			await context.SaveChangesAsync();

			return Ok();
		}
		/*
		 registrar varios generos
		 */
		[HttpPost("varios")]
		public async Task<ActionResult> Post(Genero[] generos)
		{
			/*
			 para agregarlos todos de una sola vez
			 */
			context.AddRange(generos);
			await context.SaveChangesAsync();
			return Ok();
		}

		/*
			 actualizacion de datos agregando un dato con dbcontext
		
		[HttpPost("agregar2")]
		public async Task<ActionResult>Agregar2(int id)
		{
			var genero = await context.Generos.AsTracking().FirstOrDefaultAsync(g=>g.Identificador==id);

			if (genero is null)
			{
				return NotFound();
			}

			genero.Nombre += " 2";

			context.SaveChanges();
			return Ok();

		}
		*/

		/*
		 eliminacion de datos normal
		 */
		[HttpDelete("{id:int}")]
		public async Task<ActionResult>Delete(int id)
		{
			/*
			 la importancia del await, para cuando es async
			 */
			var genero = await context.Generos.FirstOrDefaultAsync(g=>g.Identificador==id);

			if (genero is null) 
				return NotFound();

			/*
			 .remove(), es el que cambia el status para decirle a EFC que esta listo para 
			ser eliminado
			 */
			context.Remove(genero);

			await context.SaveChangesAsync();

			return Ok();
		}

		/*
		 borrado suave o logico
		 */
		[HttpDelete("borradoLogico/{id:int}")]
		public async Task<ActionResult>DeleteSuave(int id)
		{
			/*
			 .AsTracking, porque vamos actualizar, el estado del registro que vamos a borrar

			 */
			var genero = await context.Generos.AsTracking().FirstOrDefaultAsync(g => g.Identificador == id);

			if (genero is null)
				return NotFound();

			/*
			 nos pertmite cambiar los valores del campo del registro seleccionado
			 */
			genero.EstaBorrado=true;
			await context.SaveChangesAsync();
			return Ok();

		}

		/*
		  brincando o ignorando la aplicacion por defecto del filtro del modelo
		 */
		[HttpPost("restaurarBorrados/{id:int}")]
		public async Task<ActionResult>RestaurarBorrados(int id)
		{
			var genero= await context.Generos.AsTracking()
							/*
							 aca aplicamos el .IgnoreQueryFilters
							para que nos permite visualizar los datos borrados
							o quitar los filtros aplicados a nivel de la 
							configuracion del modelo
							 */
							.IgnoreQueryFilters()
							.FirstOrDefaultAsync(g=>g.Identificador==id);
			if(genero is null)
				return NotFound();

			genero.EstaBorrado = false;
			await context.SaveChangesAsync();
			return Ok();

		}

		/*
		 utilizacion de modificacion del Sobrescribir savechanges,
		 */
		[HttpPut]
		public async Task<ActionResult>Put(Genero genero)
		{
			context.Update(genero);
			await context.SaveChangesAsync();
			return Ok();
		}

		/*
		 grepoint de llamar procedimientos almancenados
		 */
		[HttpGet("procemiento_almacenado/{id:int}")]
		public async Task<ActionResult<Genero>> GetSP(int id)
		{
			var genero= context.Generos.FromSqlInterpolated($"EXEC dbo.S_GeneroObtenerPorId {id}")
						.IgnoreQueryFilters()
						.AsAsyncEnumerable();
			//debemos usar foreach ya que es un enumerable de tipo async
			await foreach (var item in genero)
			{
				return item;
			}

			return NotFound();
		}

		[HttpPost("procedimiento_almacenado")]
		public async Task<ActionResult> PostSP(Genero genero)
		{
			var existeGeneroConNombre = await context.Generos.AnyAsync(g => g.Nombre == genero.Nombre);

			if (existeGeneroConNombre)
				return BadRequest($"Ya existe un genero con ese nombre: {genero.Nombre}");

			//debemos traer el id del sql, entonces hacemos un parametro de sql
			var outputId = new SqlParameter
			{
				ParameterName = "@id",
				SqlDbType = System.Data.SqlDbType.Int,
				Direction = System.Data.ParameterDirection.Output
			};

			await context.Database.ExecuteSqlRawAsync("EXEC dbo.I_Generos @nombre={0}, @id={1} OUTPUT", genero.Nombre, outputId);

			//obtenemos el valor del id
			var id=(int)outputId.Value;

			//para visualizar el valor
			return Ok(id);

		}

		/*
		 conflitos de concurrencia por campo
		 */
		[HttpPost("concurrency_token")]
		public async Task<ActionResult> ConcurrencyToken()
		{
			var generoId = 1;

			/*
			 aqui vemos un problema de data en conflictos de concurrencia
			para evitarlo ire a la entidad
			 */

			//Jordan, lee el registro de la bd
			var genero=await context.Generos.AsTracking()
							.FirstOrDefaultAsync(g=>g.Identificador==generoId);
			genero.Nombre = "Jordan estuvo aqui";

			//Ester, actualiza el registro en la bd
			await context.Database.ExecuteSqlInterpolatedAsync($"UPDATE Generos SET Nombre='Ester estuvo aqui' WHERE Identificador={generoId}");

			//Jordan intenta actualizar
			await context.SaveChangesAsync();

			return Ok();

		}

		/*
		 conflicto de concurrencia con el modelo desconectado
		por campo
		 */
		[HttpPut("concurrenciaModeloDesconectado")]
		public async Task<ActionResult>PutModeloDesconectado(GeneroActualizacionDTO generoActualizacionDTO)
		{
			//mapeando del DTO a la entidad
			var genero=mapper.Map<Genero>(generoActualizacionDTO);

			context.Update(genero);
			context.Entry(genero).Property(g => g.Nombre).OriginalValue = generoActualizacionDTO.Nombre_Original;
			await context.SaveChangesAsync();
			return Ok();

		}

	}
}
