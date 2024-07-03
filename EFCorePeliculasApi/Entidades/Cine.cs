using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.Collections.ObjectModel;

namespace EFCorePeliculasApi.Entidades
{

    //   public class Cine
    //{
    //       public int Id { get; set; }
    //       public string? Nombre { get; set; }

    //           // para poner una precision al campo decimal

    //           //[Precision(precision:nNumerosEnteros,scale:nNumerosDecimales)]
    //           //public decimal Precio { get; set; }


    //           // para usar geolocalizacion ponemos Point de 
    //           //using NetTopologySuite.Geometries;

    // public Point? Ubicacion { get; set; }

    //           // propiedad de navegacion, nos permite realizar forening key
    //           //entre las tablas y su relacional; mecanismo que nos permite
    //           //navegar entre las relaciones

    //           //    public CineOferta CineOferta { get; set; }

    //           //virtual, nos permite el uso de la tecnica Lazy loading, para lo que 
    //           //es seleccion de datos en memoria, se usa en todas las propiedades de 
    //           //navegacion como hasset y entre otras

    //           //    public virtual CineOferta CineOferta { get; set; }

    // public CineOferta CineOferta { get; set; }

    //           // HashSet<nombreClase>, nos permite traer una coleccion del tipo
    //           //objeto que estamos poniendo, este es mas rapido, pero no trae
    //           //los elementos ordenados

    //           //    public HashSet<SalaDeCine> SalasDeCine { get; set; }

    //           //virtual, nos permite el uso de la tecnica Lazy loading, para lo que 
    //           //es seleccion de datos en memoria, se usa en todas las propiedades de 
    //           //navegacion como hasset y entre otras

    //           //public virtual HashSet<SalaDeCine> SalasDeCine { get; set; }


    // public HashSet<SalaDeCine> SalasDeCine { get; set; }
    //       //propiedad de navegacion a division de tabla
    //       public CineDetalle CineDetalle { get; set; }

    //        //configuracion para usar entidad de propiedad

    //           public Direccion Direccion { get; set; }
    //   }

    /*
     para aplicar la estrategia de deteccion de cambios personalizada
    tenemos que la clase herede de la clase base que creamos para 
    asi ponerla a funcionar, ahora debemos poner propiedades private
    de cada propiedad, de manera explicita
     */
    public class Cine : Notificacion
    {
        public int Id { get; set; }
        private string _nombre;
        public string? Nombre
        {
            get => _nombre;
            /*
             se va utilizar el set de la clase de notificacion, 
            para asi saber si el campo nombre se ha actualizado o no,
            para disparar el evento para que notifique al efc que debe actualizar
            dicho campo
             */
            set => Set(value, ref _nombre);

        }
        private Point? _ubicacion;
        public Point? Ubicacion { get => _ubicacion; set => Set(value, ref _ubicacion); }
        private CineOferta _cineOferta;
        public CineOferta CineOferta { get => _cineOferta; set => Set(value, ref _cineOferta); }
        /*
		 cuando es una lista o HasSet, debemos usar ObservableCollection para esta tecnica
        y debemos modificar algunas cosas, porque nos dara error ya que usamos este tipo de dato
		 */

        public ObservableCollection<SalaDeCine> SalasDeCine { get; set; }
        private CineDetalle _cineDetalle;
        public CineDetalle CineDetalle { get => _cineDetalle; set => Set(value, ref _cineDetalle); }
        private Direccion _direccion;
        public Direccion Direccion { get => _direccion; set => Set(value, ref _direccion); }
    }
}

