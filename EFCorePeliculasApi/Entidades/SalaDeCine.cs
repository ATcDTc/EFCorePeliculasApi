using System.ComponentModel.DataAnnotations.Schema;

namespace EFCorePeliculasApi.Entidades
{

	//public class SalaDeCine
	//{
	//	public int Id { get; set; }
	//	/*
	//	 para enlazar el enum con la clase objeto
	//	 */
	//	public TiposSalaDeCine TipoSalaDeCine { get; set; }
	//	public decimal Precio { get; set; }
	//	/*
	//        va hacer la llave foreing key, para enlazar sala con cine

	//       tambien por medio de la configuracion de notaciones de datos, podemos
	//       configurarla por medio del atributo ForeingKey, que nos permite establecer
	//       que esta propiedad de navegacion lo va hacer ForeignKey

	//           public int ElCine {get;Set;}
	//           [ForeignKey(nameof(ElCine))]

	//       esto sirve si nos queremos salir de la convencion entonces podemos usar
	//       cualquier nombre que deseemos, pero esta estara configurada por dicho atributo
	//       */

	//	public int CineId { get; set; }

	//	/*

	//       public Cine Cine { get; set; }

	//       relacion de muchos automatica
	//           public HashSet<Pelicula> Peliculas { get; set; }

	//        virtual, nos permite el uso de la tecnica Lazy loading, para lo que 
	//       es seleccion de datos en memoria, se usa en todas las propiedades de 
	//       navegacion como los HashSet y los tipo de datos declarados del tipo
	//    clase de entidad, campos de tipo List

	//           public virtual Cine Cine { get; set; }
	//           public virtual HashSet<Pelicula> Peliculas { get; set; }
	//        */
	//	public Cine? Cine { get; set; }
	//	/*
	//	 relacion de muchos automatica
	//	 */
	//	public HashSet<Pelicula>? Peliculas { get; set; }
	//	/*
	//	 agregamos un enum que va ha ser una conversion
	//	personalizada, porque vamos a guardar el simbolo
	//	de la moneda
	//	 */
	//	public Moneda Moneda { get; set; }
	//}

	/*
		se implementa dicha interfaz para que automapper funcione
	   con la estrategia de deteccion de cambios personalizada
	   con los ids
	 */
	public class SalaDeCine : IId
	{
		public int Id { get; set; }
		/*
	        para enlazar el enum con la clase objeto
	        */
		public TiposSalaDeCine TipoSalaDeCine { get; set; }
		public decimal Precio { get; set; }
		/*
	        va hacer la llave foreing key, para enlazar sala con cine

	       tambien por medio de la configuracion de notaciones de datos, podemos
	       configurarla por medio del atributo ForeingKey, que nos permite establecer
	       que esta propiedad de navegacion lo va hacer ForeignKey

	           public int ElCine {get;Set;}
	           [ForeignKey(nameof(ElCine))]

	       esto sirve si nos queremos salir de la convencion entonces podemos usar
	       cualquier nombre que deseemos, pero esta estara configurada por dicho atributo
	       */

		public int CineId { get; set; }

		/*

	       public Cine Cine { get; set; }

	       relacion de muchos automatica
	           public HashSet<Pelicula> Peliculas { get; set; }

	        virtual, nos permite el uso de la tecnica Lazy loading, para lo que 
	       es seleccion de datos en memoria, se usa en todas las propiedades de 
	       navegacion como los HashSet y los tipo de datos declarados del tipo
	    clase de entidad, campos de tipo List

	           public virtual Cine Cine { get; set; }
	           public virtual HashSet<Pelicula> Peliculas { get; set; }
	        */
		public Cine? Cine { get; set; }
		/*
	        relacion de muchos automatica
	        */
		public HashSet<Pelicula>? Peliculas { get; set; }
		/*
	        agregamos un enum que va ha ser una conversion
	       personalizada, porque vamos a guardar el simbolo
	       de la moneda
	        */
		public Moneda? Moneda { get; set; }
	}
}
