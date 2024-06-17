namespace EFCorePeliculasApi.Entidades
{
	public class PeliculaActor
	{
        /*
		 tabla relacional de muchos a muchos de manera manual
		ya que tendremos informacion personalizada que necesitaremos
		 */
        public int PeliculaId { get; set; }
        public int ActorId { get; set; }
        public string Personaje { get; set; }
        public int Orden { get; set; }
		/*
        public Pelicula Pelicula { get; set; }
        public Actor Actor { get; set; } 
        
        virtual, nos permite el uso de la tecnica Lazy loading, para lo que 
        es seleccion de datos en memoria, se usa en todas las propiedades de 
        navegacion como los HashSet y los tipo de datos declarados del tipo
	    clase de entidad, campos de tipo List

            public virtual Pelicula Pelicula { get; set; }
            public virtual Actor Actor { get; set; }
         
         */
		public Pelicula Pelicula { get; set; }
		public Actor Actor { get; set; }
	}
}
