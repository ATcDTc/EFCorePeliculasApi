namespace EFCorePeliculasApi.DTOs
{
	public class PeliculasFiltroDTO
	{
		/*
		 se pone los valores los cuales vamos a permitir filtrar
		la informacion
		 */
        public string? Titulo { get; set; }
        public int GeneroId { get; set; }
        public bool EnCartelera { get; set; }
        public bool ProximosEstrenos { get; set; }
    }
}
