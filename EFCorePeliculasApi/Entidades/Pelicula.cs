namespace EFCorePeliculasApi.Entidades
{
	public class Pelicula
	{
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public bool EnCartelera { get; set; }
        public DateTime FechaEstreno { get; set; }
        /*
         para no aceptar caracteres especiales, para 
        ahorrar espacio en la bd con el atributo
        [Unicode(false)]
        si llegaramos a guardar caracteres especiales 
        colocamos true
         */
        public string? PosterURL { get; set; }
		/*
         para relaciones de muchos a muchos de forma automatica
        para relaciones sencillas sin nada personalizado

        el tipo de dato HashSet, no garantiza simpre el orden 
        correcto de los registros, por eso se hace el cambio al tipo
        del elemento correcto de acuerdo a la situacion
        
            public HashSet<Genero> Generos { get; set; }
        por eso se hace el cambio a tipo de lista de clase

            public List<Genero> Generos { get; set; }
            public HashSet<SalaDeCine> SalasDeCine { get; set; }

        virtual, nos permite el uso de la tecnica Lazy loading, para lo que 
        es seleccion de datos en memoria, se usa en todas las propiedades de 
        navegacion como los HashSet y los tipo de datos declarados del tipo
	    clase de entidad, campos de tipo List

            public virtual List<Genero> Generos { get; set; }
            public virtual HashSet<SalaDeCine> SalasDeCine { get; set; }
         */
		public List<Genero>? Generos { get; set; }
		/*
            public HashSet<SalaDeCine> SalasDeCine { get; set; }
         el hashset, no es enumerable, entonces se hace un cambio por la necesidad actual
        y se pasa a list
         */
		public List<SalaDeCine>? SalasDeCine { get; set; }

		/*
         relacion de muchos a muchos de forma manual
        ya que tenemos campo personalizado en la tabla relacional
        y se pone la relacion a la clase objeto que hace de tabla relacional

            public HashSet<PeliculaActor> PeliculasActores { get; set; }

        virtual, nos permite el uso de la tecnica Lazy loading, para lo que 
        es seleccion de datos en memoria, se usa en todas las propiedades de 
        navegacion como los HashSet y los tipo de datos declarados del tipo
	    clase de entidad, campos de tipo List

            public virtual HashSet<PeliculaActor> PeliculasActores { get; set; }

        se hace cambio de hasset por list, ya que este primero, no es un enumerable

            public HashSet<PeliculaActor> PeliculasActores { get; set; }
         */
		public List<PeliculaActor>? PeliculasActores { get; set; }
    }
}
