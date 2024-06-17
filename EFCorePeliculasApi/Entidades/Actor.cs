namespace EFCorePeliculasApi.Entidades
{
	public class Actor
	{
        public int Id { get; set; }
        /*
         para que el nombre del actore este con la primera letra en mayusculas
        y el apellido si tuviese
        declaramos una variable private, para hacer esta modificacion a nivel del model
         */
        private string _nombre;
        public string? Nombre {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = string.Join(' ', 
                    value.Split(' ')
                        .Select(n => n[0].ToString().ToUpper() + n.Substring(1).ToLower()).ToArray());
            }
        }
        public string? Biografia { get; set; }
		/*
         para mapear el tipo de dato que va usar el campo
        
        [Column(TypeName ="Date")]
         */
		
        public DateTime? FechaNacimiento { get; set; }
		/*
         para relacionar de muchos a muchos de forma manual
        lo hacemos por medio del hasSet<> de la clase que es la 
        tabla relacional, ya que tiene campos personalizado

            public HashSet<PeliculaActor> PeliculasActores { get; set; }

        virtual, nos permite el uso de la tecnica Lazy loading, para lo que 
        es seleccion de datos en memoria, se usa en todas las propiedades de 
        navegacion como hasset y entre otras

            public virtual HashSet<PeliculaActor> PeliculasActores { get; set; }
         */
        public HashSet<PeliculaActor> PeliculasActores { get; set; }

        /*
         configuraciones automaticas con el api fluente
        lo cual si cumple con cierta condicion entonces se le aplicara
        a este campo una propiedad diferente que las demas
         */
        public string? FotoURL { get; set; }

        /*
         esta propiedad, para que no este como campo en la bd, le ponemos
        el attribute 
            [NotMapped]
        estos tipos de propiedades, por lo general se usan para generar campos
        o campos calculados
         */
        public int? Edad 
        {
            get 
            {
                if (!FechaNacimiento.HasValue)
                {
                    return null;
                }

                var fechaNacimiento=FechaNacimiento.Value;
                var edad=DateTime.Today.Year-fechaNacimiento.Year;

                if (
                    /*
                     aca declaramos un nuevo dato de tipo datetime para compararlo
                    con la fecha actual
                     */
                        new DateTime(DateTime.Today.Year,fechaNacimiento.Month,fechaNacimiento.Day) > DateTime.Today
                    )
                {
                    edad--;
                }

                return edad;
            }
        }

		/*
         para ignorar una clase completa
            public Direccion Direccion { get; set; }

        configuracion de una entidad de porpiedad
         */
		public Direccion DireccionHogar { get; set; }
        public Direccion BillingAddress { get; set; }
    }
}
