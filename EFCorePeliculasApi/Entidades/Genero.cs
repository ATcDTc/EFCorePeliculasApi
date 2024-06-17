using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EFCorePeliculasApi.Entidades
{
	/*
     vamos a configurar el esquema de la tabla y nombre con attibutte
    con [Table("nombreTabla",Schema ="nombreEsquema")]
    
        [Table("TablaGeneros",Schema ="peliculas")]

    tambien podemos configurar indices personalizados de otros tipos 
    de campos, para asi realizar consultas mas rapidas

        [Index(nameof(Nombre),IsUnique =true)]

    sobrescribir SaveChanges
    por eso hereda de una clase para esto
     */
	public class Genero:EntidadAuditable
	{
		/*
         para crear una llave primaria
         podemos usar identidades en llaves primarias
        o podemos usar un attributte - [Key]
        o por medio de api fuente
         */
		//[Key]
        public int Identificador { get; set; }
		/*
         para configurar el limite de las dimensiones del campo
        usamos atributo
            [StringLength(150)]
            [MaxLength(150)]
        en el caso de un string ambos hacen lo mismo
         */
		/*
         para hacer que los campos no permitan nulos
        ponemos [Required] atributo
         */
		/*
         para ponerle cualquier nombre a la columna
        distinta al nombre de la variable de la clase
        
        [Column("NombreGenero")]
         */
		public string? Nombre { get; set; }
        /*
         relacion de muchos a muchos de forma automatica
        de manera sencilla, sin campos personalizados

            public HashSet<Pelicula> Peliculas{ get; set; }

        virtual, nos permite el uso de la tecnica Lazy loading, para lo que 
        es seleccion de datos en memoria, se usa en todas las propiedades de 
        navegacion como hasset y entre otras

            public virtual HashSet<Pelicula> Peliculas { get; set; }
         */

        /*
         se agrega este campo en la tabla de tipo bool, para aplicar el borrado 
        suave o logico, para no eliminar los registros de la bd de forma permanete
        sino para que estos puedan quedar como registros de datos estadisticos
         */
        public bool EstaBorrado { get; set; }
        public HashSet<Pelicula>? Peliculas { get; set; }

        /*
         ejm comandos
        add-migration
        get-migration
       
            [MaxLength(20)]
            public string? Ejemplo { get; set; }
       
         ejm comnados
        remove-migration
        
            [MaxLength(50)]
            public string? Ejemplo2 { get; set; }
        */
    }
}
