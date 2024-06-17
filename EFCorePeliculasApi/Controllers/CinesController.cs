using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCorePeliculasApi.DTOs;
using EFCorePeliculasApi.Entidades;
using EFCorePeliculasApi.Entidades.SinLlaves;
using EFCorePeliculasApi.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System.Collections.ObjectModel;

namespace EFCorePeliculasApi.Controllers
{
	[ApiController]
	[Route("api/cines")]
	public class CinesController : ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;
		private readonly IActualizarObservableCollection actualizarObservableCollection;

		public CinesController(ApplicationDbContext context, IMapper mapper
			/*
			 agregando el servicio personalizado de deteccion de cambios personalizado
				, IActualizarObservableCollection actualizarObservableCollection
			 */
			
			)
		{
			this.context = context;
			this.mapper = mapper;
			this.actualizarObservableCollection = actualizarObservableCollection;
		}

		/*
		 get de una entidad sin llave primaria, sino de tipo 
		query
		 */
		[HttpGet("sinUbicacion")]
		public async Task<IEnumerable<CineSinUbicacion>> GetCineSinUbicacion()
		{
			/*
			 como ya tiene un query
			
				return await context
						
						 set<>, nos permite en tiempo real, crear un dbconext
						para la clase sin llaves, ya que en el api fluente no 
						se creo una propiedad del tipo dbset de la entidad sin
						llaves

							.Set<CineSinUbicacion>().ToListAsync();
			
			 o configurarlo desde el api fluente como propiedad de tipo dbset
				return await context.CineSinUbicacion.ToListAsync();
			 */
			return await context.CineSinUbicacion.ToListAsync();
		}

		[HttpGet]
		public async Task<IEnumerable<CineDTO>> Get()
		{
			return await context.Cines.ProjectTo<CineDTO>(mapper.ConfigurationProvider).ToListAsync();
		}

		[HttpGet("cercanos")]
		public async Task<ActionResult> Get(double latitud, double longitud)
		{
			//configuramos el GEOMETRYFACTORY con srid: 4326, nos permite hacer mediciones 
			// en nuestro planeta tierra
			var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

			//podemos agregar una distancia maxima en metros
			var distanciaMaximaEnMetros = 2000;

			var miUbicacion = geometryFactory.CreatePoint(new Coordinate(longitud, latitud));

			//buscando el cine mas cercano a mi ubicacion
			var cines = await context.Cines
				.OrderBy(c => c.Ubicacion.Distance(miUbicacion))
				//filtraremos por un top maximo de distancia
				.Where(
					n => n.Ubicacion.IsWithinDistance(miUbicacion, distanciaMaximaEnMetros)//nos permite decirle mientras este alrededor de o menor o igual que
				)
				//hacemos una proyeccion para mostrar ciertos datos
				.Select(n => new
				{
					Nombre = n.Nombre,
					Distancia = Math.Round(n.Ubicacion.Distance(miUbicacion))
				}
				).ToListAsync();
			;
			return Ok(cines);
		}

		[HttpPost]
		public async Task<ActionResult> Post()
		{
			var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
			var ubicacionCine = geometryFactory.CreatePoint(new Coordinate(-85.43760300030577, 10.624771908018717));

			var cine = new Cine
			{
				Nombre = "Le Cine con detalle",
				Ubicacion = ubicacionCine,
				/*
				 agregando data en las columnas que se agregaron con 
				la tecnica de division de tablas
				 */
				CineDetalle=new CineDetalle()
				{
					Historia="hISTORIA...",
					CodigoEtica="Código...",
					Misiones="Misiones..."
				},
				/*
				 aca esto es data relacionada, qu epodemos
				integrarla aqui mismo
				 */
				CineOferta = new CineOferta
				{
					PorcentajeDescuento = 5,
					FechaInicio = DateTime.Today,
					FechaFinal = DateTime.Today.AddDays(8)
				},
				
				 //podemos crear nuestra sala de cines, con hashset

					SalasDeCine = new HashSet<SalaDeCine>
					{
						new SalaDeCine
						{
							Precio=200,
							Moneda=Moneda.Desconocida,
							TipoSalaDeCine=TiposSalaDeCine.DosDimensiones
						},
						new SalaDeCine
						{
							Precio=350,
							Moneda=Moneda.BTC,
							TipoSalaDeCine=TiposSalaDeCine.TresDimensiones
						}
					}
				 

				/*
				 se hace un cambio por ObservableCollection, ya que se esta usando una tecnica
				de estrategia de deteccion de cambios personalizado
				 
				SalasDeCine = new ObservableCollection<SalaDeCine>
				{
					new SalaDeCine
					{
						Precio=200,
						Moneda=Moneda.ColonCostarricense,
						TipoSalaDeCine=TiposSalaDeCine.DosDimensiones
					},
					new SalaDeCine
					{
						Precio=350,
						Moneda=Moneda.BTC,
						TipoSalaDeCine=TiposSalaDeCine.TresDimensiones
					}
				}
				*/
			};

			context.Add(cine);
			await context.SaveChangesAsync();
			return Ok();
		}

		/*
		 usando DTOs, para ingresar datos
		 */
		[HttpPost("conDTO")]
		public async Task<ActionResult> Post(CineCreacionDTO cineCreacionDTO)
		{
			var cine = mapper.Map<Cine>(cineCreacionDTO);

			context.Add(cine);
			await context.SaveChangesAsync();
			return Ok();

		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult> Get(int id)
		{
			/*
				var cineDB = await context.Cines
								.Include(c => c.SalasDeCine)
								.Include(c => c.CineOferta)
								
									 incluyendo los campos de la tecnica
									de la tabla dividida
								
								.Include(c=>c.CineDetalle)
								.FirstOrDefaultAsync(c => c.Id == id);

			
			 usando qyerues arbitrarios con .FromSqlInterpolated()
				junto con LINQ
			 */
			var cineDB = await context.Cines
							.FromSqlInterpolated($"SELECT TOP 1 * FROM Cines WHERE Id={id}")
							//agregamos la data relacionada del registro
							.Include(c => c.SalasDeCine)
							.Include(c => c.CineOferta)
							.Include(x => x.CineDetalle)
							.FirstOrDefaultAsync();


			if (cineDB is null)
				return NotFound();

			cineDB.Ubicacion = null;

			return Ok(cineDB);
		}

		/*
		 actualizando entidad principal y su data relacionada
		 */
		[HttpPut("{id:int}")]
		public async Task<ActionResult> Put(CineCreacionDTO cineCreacionDTO, int id)
		{
			var cineBD = await context.Cines.AsTracking()
							.Include(c => c.SalasDeCine)
							.Include(c => c.CineOferta)
							.FirstOrDefaultAsync(c => c.Id == id);
			if (cineBD is null)
			{
				return NotFound();
			}
			/*
			 esta linea lo hace todo, CUD
			 */
			cineBD = mapper.Map(cineCreacionDTO, cineBD);

			/*
			 pasaremos las entidades y el dto al servicio de deteccion de cambios personalizado
				actualizarObservableCollection.Actualizar(cineBD.SalasDeCine, cineCreacionDTO.SalasDeCine);
			 */
			//actualizarObservableCollection.Actualizar(cineBD.SalasDeCine, cineCreacionDTO.SalasDeCine);

			await context.SaveChangesAsync();

			return Ok();
		}

		/*
		 actualizando solo data relacionada sola sin la
		entidad principal
		 */
		[HttpPut("cineOferta")]
		public async Task<ActionResult> PutCineOferta(CineOferta cineOferta)
		{
			context.Update(cineOferta);
			await context.SaveChangesAsync();
			return Ok();
		}

		/*
		 borrado de prueba con las relaciones requeridas
		 */
		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id)
		{
			var cine = await context.Cines
				/*
				 cuando se configura en el api fluente
				OnDeletd, del tipo Restrict, para poder borrar
				entidades dependientes, de la entidad principal, 
				debemos agregarlas por medio de .Include()
				 */
				.Include(c=>c.SalasDeCine)
				/*
				 cuando un campo es relacional pero de tipo 
				opcional, que es decir que puede ser nulo,
				este al eliminarlo, debemos incluirlo con 
				include()
					.Include(c=>c.CineOferta)
				despues de borrado, este campo queda como 
				null, ya que la entidad principal, ya no 
				existe, en otras palabras queda huerfana
				 */
				.FirstOrDefaultAsync(c => c.Id == id);

			if (cine is null)
				return NotFound();

			/*
			 borrando las salas de cine por medio de .RemoveRange()
			ya que va una a una
			 */
			context.RemoveRange(cine.SalasDeCine);
			await context.SaveChangesAsync();

			/*
			 despues ya en si borra el cine en si
			 */
			context.Remove(cine);
			await context.SaveChangesAsync();
			return Ok();
		}
	}
}
