using EFCorePeliculasApi.Entidades;
using System.Collections.ObjectModel;

namespace EFCorePeliculasApi.Servicios
{
	public interface IActualizarObservableCollection
	{
		void Actualizar<Ent, DTO>(ObservableCollection<Ent> entidades, IEnumerable<DTO> dtos)
			where Ent : IId
			where DTO : IId;
	}
}
