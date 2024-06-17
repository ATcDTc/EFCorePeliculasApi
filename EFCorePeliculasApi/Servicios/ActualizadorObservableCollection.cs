using AutoMapper;
using EFCorePeliculasApi.Entidades;
using System.Collections.ObjectModel;

namespace EFCorePeliculasApi.Servicios
{
	/*
	 este servicio actualiza un ObservableCollection, 
	a partir de un enum
	 */
	public class ActualizadorObservableCollection:IActualizarObservableCollection
	{
		private readonly IMapper mapper;

		public ActualizadorObservableCollection(IMapper mapper)
        {
			this.mapper = mapper;
		}

		public void Actualizar<Ent,DTO>(ObservableCollection<Ent> entidades, IEnumerable<DTO> dtos)
			/*
			 garantizamo que Ent y DTO, implemente la interfaz IId
			 */
			where Ent:IId
			where DTO : IId
		{
			/*
			 algoritmo que crea, actualiza o borrar entidades
			 */
			var diccionarioEntidades = entidades.ToDictionary(x => x.Id);
			var diccionarioDTOs=dtos.ToDictionary(x => x.Id);

			//seleccionaremos la llave del diccionario que es el id
			var idsEntidades = diccionarioEntidades.Select(x => x.Key);
			var idsDTOs=diccionarioDTOs.Select(x => x.Key);

			/*
			 vamos a crea tres colecciones, una que es de crear, nuevas entidades
			las que estan en DTOs pero que no estan en el listado de las entidades
			*/
			var crear=idsDTOs.Except(idsEntidades);
			/*
			 borrar que es lo contrario que no estan en DTOs sino en entidades
			 */
			var borrar=idsEntidades.Except(idsDTOs);
			/*
			 actualizar que es la interseccion entre entidades y dtos
			 */
			var actualizar = idsEntidades.Intersect(idsDTOs);

			foreach (var id in crear) 
			{
				/*
				 para crear una entidad entonces hacemos
				 */
				var entidad = mapper.Map<Ent>(diccionarioDTOs[id]);
				/*
				 agregamos la entidad a agregar
				 */
				entidades.Add(entidad);
			}

			foreach (var id in borrar)
			{
				/*
				 para borrar la entidad 
				 */
				var entidad=diccionarioEntidades[id];
				entidades.Remove(entidad);
			}

			foreach(var id in actualizar)
			{
				var dto = diccionarioDTOs[id];
				var entidad = diccionarioEntidades[id];
				entidad=mapper.Map(dto,entidad);
			}

		}

    }
}
