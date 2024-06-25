using AutoMapper;
using EFCorePeliculasApi.DTOs;
using EFCorePeliculasApi.Entidades;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace EFCorePeliculasApi.Servicios
{
	public class AutoMapperProfiles:Profile
	{
        public AutoMapperProfiles()
        {
            /*
             aca decimos de donde a donde va la proyeccion, de que clase a que clase
             */
            CreateMap<Actor, ActorDTO>();
            /*
             creando un automapper, del cine
             */
            CreateMap<Cine, CineDTO>()
                /*
                 .ForMember, nos permite agregar parametros para los campos
                de la clase DTO
                 */
                .ForMember(
                    dto => dto.Latitud,
                    ent => ent.MapFrom(prop => prop.Ubicacion.Y)//nos permite tomar del campo original, una parte
                    )
                .ForMember(dto => dto.Longitud, ent => ent.MapFrom(prop => prop.Ubicacion.X));
            CreateMap<Genero, GeneroDTO>();

            /*
             codigo sin .ProjectTo

            CreateMap<Pelicula, PeliculaDTO>()
                
                 mapeo personalizado
                 
                    .ForMember(dto => dto.Cines, ent => ent.MapFrom(
                    
                         hacemos un select a la clase que ocupamos
                     
                            prop=>prop.SalasDeCine.Select(s => s.Cine)
                            ))
                    .ForMember(dto=>dto.Actores, ent=>ent.MapFrom(prop=>prop.PeliculasActores.Select(pa=>pa.Actor)));
            */

            /*
             codificacion con ProjectTo
             */
            CreateMap<Pelicula, PeliculaDTO>()
                .ForMember(dto => dto.Generos, ent => ent.MapFrom(prop => prop.Generos
                    /*
                     para ordenarlos de acuerdo a nuestras necesidades
                     */
                    .OrderByDescending(g => g.Nombre)))
                .ForMember(dto => dto.Cines, ent => ent.MapFrom(prop => prop.SalasDeCine.Select(s => s.Cine)))
                .ForMember(dto => dto.Actores, ent => ent.MapFrom(prop => prop.PeliculasActores.Select(pa => pa.Actor)));

			/*
             para la creacion de salas de cines y cines
            se va a hacer una configuracion especial para lo que es la ubicacion
             */
			var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            CreateMap<CineCreacionDTO, Cine>()
				/*
                 vamos a ignorar la entidad salas de cines para probar la deteccion de cambios personalizada
                    .ForMember(ent=>ent.SalasDeCine, op=>op.Ignore())
                 */
				//.ForMember(ent=>ent.SalasDeCine, op=>op.Ignore())
                .ForMember(ent => ent.Ubicacion, dto => dto.MapFrom(
                    /*para la factorizacion comun de las coordenadas*/
                    campo=>geometryFactory.CreatePoint(new Coordinate(campo.Longitud,campo.Latitud))
                    ));
            CreateMap<CineOfertaCreacionDTO, CineOferta>();
            CreateMap<SalaDeCineCreacionDTO,SalaDeCine>();

            /*
             insercion de datos con informacion existente
             */
            CreateMap<PeliculaCreacionDTO, Pelicula>()
                .ForMember(ent=>ent.Generos,
                    /*
                     mapeando los generos, para un tipo de lista de enteros
                     */
                    dto=>dto.MapFrom(campo=>campo.Generos.Select(id=>
                        new Genero ()
                        {
                            Identificador= id
                        }
                        )))
                .ForMember(ent=>ent.SalasDeCine, dto=>dto.MapFrom(campo=>campo.SalasDeCine.Select(id=>new SalaDeCine() { Id=id})));
            
            CreateMap<PeliculaActorCreacionDTO, PeliculaActor>();

            CreateMap<ActorCreacionDTO, Actor>();

            /*
             creacion de map para dto con control de conflicto de concurrencia con el modelo desconectado
             */
            CreateMap<GeneroActualizacionDTO, Genero>();
		}
    }
}
