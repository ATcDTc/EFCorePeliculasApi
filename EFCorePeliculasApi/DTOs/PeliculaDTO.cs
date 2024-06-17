namespace EFCorePeliculasApi.DTOs
{
	public class PeliculaDTO
	{
        public int Id { get; set; }
        public string? Titulo { get; set; }
		/*
         con ICollection<nombreClaseDTO> nos permite crear una coleccion de los datos
        como tipo de dato

        si no queeremos cambiar el tipo de ICollection, entonces podemos inicializarlo 
        como una lista() del tipo de clase
         */
		public ICollection<GeneroDTO> Generos { get; set; } = new List<GeneroDTO>();
        public ICollection<CineDTO> Cines { get; set; }
        public ICollection<ActorDTO> Actores { get; set; }
    }
}
