using EFCorePeliculasApi.Entidades;

namespace EFCorePeliculasApi.DTOs
{
	//public class SalaDeCineCreacionDTO
	//{
	//	public int Id { get; set; }
	//	public decimal Precio { get; set; }
	//	public TiposSalaDeCine TipoSalaDeCine { get; set; }

	//   }
	/*
	 se implementa dicha interfaz para que automapper funcione
	con la estrategia de deteccion de cambios personalizada
	con los ids
	 */
	public class SalaDeCineCreacionDTO : IId
	{
		public int Id { get; set; }
		public decimal Precio { get; set; }
		public TiposSalaDeCine? TipoSalaDeCine { get; set; }
	}
}
