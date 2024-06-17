using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCorePeliculasApi.DTOs;
using EFCorePeliculasApi.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCorePeliculasApi.Controllers
{
	[ApiController]
	[Route("api/actores")]
	public class ActoresController:ControllerBase
	{
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;

		public ActoresController(
			ApplicationDbContext context,
			IMapper mapper
			)
        {
			this.context = context;
			this.mapper = mapper;
		}

		[HttpGet]
		/*
		public async Task<ActionResult> Get()
		{
		
			 .ToListAsync(), trae todos los campos de la tabla
				return await context.Actores.ToListAsync();

			.Select(), podemos proyectar la informacion que necesitamos mostrar
			 

			var actores = await context.Actores.Select(
				
				 podemos hacer una proyeccion con un objeto anonimo
					a=> new {Id=a.Id, Nombre=a.Nombre}
				o a un DTO
				
		}
		*/
		/*
		 hacemos un select, con proyeccion a un DTO
		public async Task<IEnumerable<ActorDTO>> Get()
		{
		 
			return await context.Actores.Select(
				a=>new ActorDTO { Id=a.Id, Nombre=a.Nombre}
					).ToListAsync();
		}

		*/
		public async Task<IEnumerable<ActorDTO>> Get()
		{
			/*
			 despues de instalar y configurar AutoMapper,
			podemos hacer la proyeccion con el DTO
			 */
			return await context.Actores
				/*
				 con .ProyectTo<nombreHaciaDondeProyectamos>, decimos
				hacia donde proyectaremos
				 */
				.ProjectTo<ActorDTO>(mapper.ConfigurationProvider)
				.ToListAsync();
		}

		[HttpPost]
		public async Task<ActionResult> Post(ActorCreacionDTO actorCreacionDTO)
		{
			var actor = mapper.Map<Actor>(actorCreacionDTO);

			context.Add(actor);
			await context.SaveChangesAsync();
			return Ok(actor);
		}

		/*
		 actualizar registros existentes
		por eso se usa HttpPut()
		 */
		[HttpPut("{id:int}")]
		public async Task<ActionResult>Put(ActorCreacionDTO actorCreacionDTO, int id)
		{
			var actorDb=await context.Actores.AsTracking().FirstOrDefaultAsync(a=>a.Id == id);

			if (actorDb is null)
			{
				return NotFound();
			}

			/*
			actualizacion con automapper,
			estamos mapeando de actorCreacionDTO a actorDb,
			esto quiere decir que actorCreacionDTO, viene con un nombre nuevo
			y actorDb, tiene el nombre anterior, entonces este mapeo
			le agrega el nombre nuevo que esta en actorCreacionDTO a actorDb
			
			esto es para que automapper, tenga la misma instancia de actorDb 
			en memoria, es decir que solo estamos modificando, las propiedades 
			de actorDb, y no estamos cambiando la instancia del actorDb, es util
			porque entity core, le sigue dando seguimiento a la instancia de
			actorDb, asi se dara cuenta que esta el estatus modificado, y asi se
			pueda aplicar los cambios en la bd

			 */
			actorDb = mapper.Map(actorCreacionDTO,actorDb);

			/*
			 estrategia de deteccion de cambios personalizados
			-currentvalues, es igual a valores actuales

				var entry=context.Entry(actorDb);
			 */

			await context.SaveChangesAsync();
			return Ok();
		}

		/*
		 actualizacion de registros, de manera desconectado
		 */
		[HttpPut("desconectado/{id:int}")]
		public async Task<ActionResult>PutDesconectado(ActorCreacionDTO actorCreacionDTO, int id)
		{
			var existenciaActores = await context.Actores.AnyAsync(a=>a.Id==id);

			if (!existenciaActores)
			{
				return NotFound();
			}

			var actor = mapper.Map<Actor>(actorCreacionDTO);
			actor.Id= id;

			/*
			 .update, nos permite marcar el objeto como modificado, para
			que sus propiedades han sido modificada por lo tanto debe
			estar preparado para su actualizacion
				
				context.Update(actor);
			 */

			/*
			 actualizando algunas propiedades, pero las demas no estan modificadas
			esto nos permite solo actualizar las que le configuremos y no todas las 
			demas de la tabla de sus columnas de sus bd

				context.Entry(actor).Property(actor=>actor.Nombre).IsModified=true;
			 */
			context.Entry(actor).Property(actor=>actor.Nombre).IsModified=true;

			await context.SaveChangesAsync();

			return Ok();

		}
	}
}
