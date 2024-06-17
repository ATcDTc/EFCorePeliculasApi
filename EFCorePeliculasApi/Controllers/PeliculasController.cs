using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCorePeliculasApi.DTOs;
using EFCorePeliculasApi.Entidades;
using EFCorePeliculasApi.Entidades.SinLlaves;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculasApi.Controllers
{
	[ApiController]
	[Route("api/peliculas")]
	public class PeliculasController:ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		public PeliculasController(ApplicationDbContext context, IMapper mapper)
        {
			this.context = context;
			this.mapper = mapper;
		}

		/*
		 utilizando una vista que esta en el sqlserver, con entidades sin llaves
		 */
		[HttpGet("peliculasConConteos")]
		public async Task<ActionResult<IEnumerable<PeliculaConConteos>>>GetPeliculasConConteos()
		{
			return await context.PeliculaConConteos.ToListAsync();
		}


		[HttpGet("{id:int}")]
		public async Task<ActionResult<PeliculaDTO>> Get(int id)
		{
			var pelicula=await context.Peliculas
				.Include(p=>p.Generos
				/*
				 podemos ordernar los registros de acuerdo a lo que ocupamos
				 */
				.OrderByDescending(g=>g.Nombre)
					)
				.Include(p=>p.SalasDeCine)
					/*
					 nos permite saber la informacion de la tabla padre
					de la tabla hija, con que esta relacionada esta clase
					 */
					.ThenInclude(s=>s.Cine)
				.Include(p=>p.PeliculasActores
				/*
				 tambien podemos filtrar por algun tipo en especifico
				de condicion con la claupsula .Where()
				 */
				.Where(pa=>pa.Actor.FechaNacimiento.Value.Year>=1980)
					)
					.ThenInclude(pa=>pa.Actor)
				.FirstOrDefaultAsync(p=>p.Id==id);

			if (pelicula is null)
			{
				return NotFound();
			}

			/*
			 sin uso de .projectTo
			 */
			var peliculaDTO=mapper.Map<PeliculaDTO>(pelicula);

			/*si se nos ha duplicado los datas usamos un .DistinctBy*/
			peliculaDTO.Cines=peliculaDTO.Cines.DistinctBy(c=>c.Id).ToList();

			return peliculaDTO;
		}

	
		/*
			 utilizacion completa del automapper
		
		[HttpGet("conProjectTo/{id:int}")]
		public async Task<ActionResult<PeliculaDTO>> GetProjectTo(int id)
		{
			var pelicula = await context.Peliculas
				.ProjectTo<PeliculaDTO>(mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(p => p.Id == id);

			if (pelicula is null)
			{
				return NotFound();
			}

			
				si se nos ha duplicado los datas usamos un .DistinctBy
			pelicula.Cines = pelicula.Cines.DistinctBy(c => c.Id).ToList();

			return pelicula;
		}

		
			 cargando informacion con .Select()
		
		[HttpGet("cargadorSelectivo/{id:int}")]//se usa ActionResult, porque se va ha cambiar la estructura del dato recibido
		public async Task<ActionResult> GetSelectivo(int id)
		{
			var peliculas = await context.Peliculas.Select(
				
						proyeccion de tipo anonimo
				 
					p => new
					{
						Id= p.Id,
						Titulo= p.Titulo,
						Generos= p.Generos
						
							 podemos aplicarle un orden a la lista
						
							.OrderByDescending(g=>g.Nombre)
						
							 si solo queremos la informacion de un campo y no todo los campos
						
							.Select(g=>g.Nombre)
								.ToList(),
						
							.Count(), si queremos una cantidad de actores o contar campos
		
						CantidadDeActores=p.PeliculasActores.Count(),
						CantidadCines=p.SalasDeCine.Select(c=>c.CineId).Distinct().Count()
					}
				).FirstOrDefaultAsync(p=>p.Id==id);

			if (peliculas is null)
				return NotFound();

			return Ok(peliculas);
		}

		  explicit loading
		
		[HttpGet("cargadoExplicito/{id:int}")]
		public async Task<ActionResult<PeliculaDTO>>GetExplicito(int id)
		{
			var pelicula= await context.Peliculas.AsTracking().FirstOrDefaultAsync(p=>p.Id==id);
			
				 aca no se carga ninguna data relacionada, sino que se carga en una linea posterior

				aca cargamos la data relacionada con la entidad principal
				.Entry, nos permite introduccirle la informacion de la data relacional que ocuparemos

				.LoadAsync(), nos permite cargar los datos relacionales del registro relacionado

				ejm:
					await context.Entry(pelicula).Collection(p=>p.Generos).LoadAsync();
			
					 podemos cargar la cantidad de registros de una entidad relacionada, para no mostrar los datos
					para eso utilizamos el .Query(), junto con .CountAsync()
			
			var cantidadGeneros = await context.Entry(pelicula).Collection(p=>p.Generos).Query().CountAsync();

			if (pelicula is null) 
				return NotFound();

			
				 usamos automapper para mapear la pelicula
			
			var peliculaDTO=mapper.Map<PeliculaDTO>(pelicula);

			return Ok(peliculaDTO);
		}

			 uso de lazy loading, que carga y usa la data en memoria
		
		[HttpGet("lazyLoading/{id:int}")]
		public async Task<ActionResult<PeliculaDTO>> GetLazyLoading(int id)
		{
			var pelicula=await context.Peliculas.AsTracking().FirstOrDefaultAsync(p=>p.Id==id);

			if (pelicula is null)
				return NotFound();

			var peliculaDTO= mapper.Map<PeliculaDTO>(pelicula);
			
		//para aplicar distinctBy, debemos usar la clase que se usa el distinctBy
			peliculaDTO.Cines=peliculaDTO.Cines.DistinctBy(c=>c.Id).ToList();

			return Ok(peliculaDTO);
		}
		
		 ejm, de lazy loading, es el problema de n+1
		
		[HttpGet("lazyLoadingEjm")]
		public async Task<ActionResult<List<PeliculaDTO>>> GetLazyLoading()
		{
			var peliculas=await context.Peliculas.AsTracking().ToListAsync();

			foreach (var pelicula in peliculas)
			{
				
				 aca cargaremos los generos de la pelicula
				 pero aca inicia el problema de n+1, porque por cada pelicula que tengamos
				relacionada con cada uno de sus generos lo consultara
				
					pelicula.Generos.ToList();
			}

			var peliculasDTOs= mapper.Map<List<PeliculaDTO>>(peliculas);
			return peliculasDTOs;
		}
		

		[HttpGet("agrupadaPorEstreno")]
		public async Task<ActionResult> GetAgrupadasPorCartelera()
		{
			var peliculasAgrupadas = await context.Peliculas
				
					 aca realizamos el agrupamiento, si tienen el mismo 
					valor en un campo especifico se agrupan en un mismo lugar
				
				.GroupBy(p=>p.EnCartelera)
				
					obligatoriamente, se debe poner un .Select() para projeccion
				 
				.Select(p=> new
				{
					
						Key, se refiere al valor que esta en el .GroupBy(), como llave
						de los elementos agrupado
					
					EnCartelera=p.Key,
					Conteo=p.Count(),
					
						 podemos obtener el listado de las peliculas de cada grupo
					
					Peliculas=p.ToList()
				}).ToListAsync();
			
			return Ok(peliculasAgrupadas);
		}

		[HttpGet("agrupadasPorCantidadDeGeneros")]
		public async Task<ActionResult> GetAgrupadasPorGenero()
		{
			var peliculasAgrupadas = await context.Peliculas
				
					agrupamos por la cantidad de generos, por eso se pone el 
					.count()
				 
				.GroupBy(p=>p.Generos.Count())
				.Select(p => new
				{
					Conteo= p.Key,
					
						 hacemos una proyeccion porque solo queremos los nombre
						no toda la data
					
					Titulos=p.Select(n=>n.Titulo),
					
						para obtener los nombres de los  generos o campo que estamos agrupando
					 
					Generos=p.Select(g=>g.Generos)
					
						realizamos un .SelectMany(), para que nos tire una sola coleccion de datos
						ya  que solo queremos no varias colecciones sino una compacta y simplificada
					 
									.SelectMany(gp=>gp)
									
									 para solo obtener los nombres, sin repeticiones
									 
									.Select(gp => gp.Nombre).Distinct()
				}).ToListAsync();

			return Ok(peliculasAgrupadas);
		}
		*/

		/*
		 ejecucion diferida
		 */
		[HttpGet("filtrar")]
		public async Task<ActionResult<List<PeliculaDTO>>> Filtrar(
			/*
			 [FromQuery], nos permite recibir un tipo de dato complejo
			para que nos permita recibir los valores de la clase que se encuentra
			en el DTO, por medio de queries string
			 */
			[FromQuery] PeliculasFiltroDTO peliculasFiltroDTO)
		{
			/*
			 .AsQueryable(), nos permite crear nuestros queries, por medio 
			de esta variable, podemos pasar nuestro queries paso a paso, 
			y ejecutarlo al final en una sola ejecucion
			 */
			var peliculasQueryable = context.Peliculas.AsQueryable();

			/*
			 aca filtraremos de acuerdo a lo solicitado por el usuario para 
			el respectivo filtrado
			 */
			if (!string.IsNullOrEmpty(peliculasFiltroDTO.Titulo))
			{
				peliculasQueryable = peliculasQueryable.Where(p => p.Titulo.Contains(peliculasFiltroDTO.Titulo));
			}
			if (peliculasFiltroDTO.EnCartelera)
			{
				peliculasQueryable = peliculasQueryable.Where(p => p.EnCartelera);
			}
			if (peliculasFiltroDTO.ProximosEstrenos)
			{
				/*
				 comparacion de fechas y toma de la fecha de hoy
				 */
				var hoy = DateTime.Today;
				peliculasQueryable = peliculasQueryable.Where(p => p.FechaEstreno > hoy);
			}
			/*
			 filtro con data relacionada
			 */
			if (peliculasFiltroDTO.GeneroId!=0)
			{
				peliculasQueryable = peliculasQueryable
					.Where(p => p.Generos
						/*
						 podemos realizar un .Select() dentro de otra fn
						 */
						.Select(g=>g.Identificador).Contains(peliculasFiltroDTO.GeneroId));
			}

			/*
			 mapeo, en donde podemos hacer .Include
			 */
			var peliculas = await peliculasQueryable.Include(p => p.Generos).ToListAsync();

			return mapper.Map<List<PeliculaDTO>>(peliculas);
		}

		[HttpPost]
		public async Task<ActionResult>	Post(PeliculaCreacionDTO peliculasCreacionDTO)
		{
			/*
			 ingreso de pelicula con generos preexistentes
			 */
			var pelicula= mapper.Map<Pelicula>(peliculasCreacionDTO);
			/*
			 para decirle a entity que son datos de consulta
			usamos .State y EntityState.Unchanged, para poderlos
			usarl sin hacerles ningun típo de modificacion, pero
			si va hacerles, la relacion entre la nueva pelicula y sus generos
			ya existentes
			 */
			pelicula.Generos.ForEach(g=>context.Entry(g).State= EntityState.Unchanged);
			/*
			 el hashset, no es enumerable
			 */
			pelicula.SalasDeCine.ForEach(sc=>context.Entry(sc).State=EntityState.Unchanged);

			/*
			 la relacion de pelicula y peliculas actores es de n a n
			entonces si este esta null, entonces es de crear un nuevo registro
			 */
			if (pelicula.PeliculasActores is not null)
			{
				//si tiene datos
				for (int i = 0; i < pelicula.PeliculasActores.Count; i++)
				{
					pelicula.PeliculasActores[i].Orden = i + 1; //modificacion del orden de ingreso
				}
			}

			context.Add(pelicula);
			await context.SaveChangesAsync();
			return Ok();
		}

	}
}
